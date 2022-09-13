using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private bool enableWallSelection = false;
    [SerializeField] private GameObject[] walls;
    public bool releaseWalls = false;
    private List<Rigidbody> rbs;

    private void Start()
    {
        rbs = new List<Rigidbody>();
        rbs.AddRange(transform.GetComponentsInChildren<Rigidbody>());
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void Update()
    {
        if(releaseWalls)
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.constraints = RigidbodyConstraints.None;

            }
            if (enableWallSelection)
            {
                foreach (GameObject wall in walls)
                {
                    SubFracture sb = wall.GetComponentInChildren<SubFracture>();
                    sb.grounded = false;
                    Rigidbody rb = wall.GetComponentInChildren<Rigidbody>();
                    rb.useGravity = true;
                }
            } else
            {
                

            }

        } 
    }
}
