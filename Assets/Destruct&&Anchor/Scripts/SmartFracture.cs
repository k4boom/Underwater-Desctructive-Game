using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFracture : MonoBehaviour
{
    public float breakRadius = .2f;
    public float breakForce = 100;

    private List<SubFracture> cells;

    void Start()
    {
        InitSubFractures();
    }

    void InitSubFractures()
    {
        cells = new List<SubFracture>();
        cells.AddRange(transform.GetComponentsInChildren<SubFracture>());

        // Find all adjacent cells for every cell
        foreach (SubFracture cell in cells)
        {
            BoxCollider tempCollider = cell.gameObject.AddComponent<BoxCollider>();

            // Test if other cells are within bounding box, then add to subfracture connections list
            Collider[] hitColliders = Physics.OverlapBox(cell.transform.position, tempCollider.size / 2, cell.transform.rotation);
            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].GetComponent<SubFracture>() && hitColliders[i].transform.root == cell.transform.root && hitColliders[i].gameObject != cell.gameObject) // Make sure it is a sub fracture of this object
                {
                    cell.connections.Add(hitColliders[i].GetComponent<SubFracture>());
                    hitColliders[i].GetComponent<SubFracture>().connections.Add(cell);
                }
                i++;
            }


            Destroy(tempCollider); // Destroy temp colliders one frame after calculations
        }
    }


    public void Fracture(Vector3 point, Vector3 force)
    {
        foreach (SubFracture cell in cells)
        {
            if (Vector3.Distance(cell.transform.position, point) < breakRadius)
            {
                // If a given cell is close to the collision, free it.
                cell.connections = new List<SubFracture>();
                cell.grounded = false;
                cell.GetComponent<Rigidbody>().isKinematic = false;
                cell.GetComponent<Rigidbody>().AddForceAtPosition(force, point, ForceMode.Force);
            }
        }
    }
}
