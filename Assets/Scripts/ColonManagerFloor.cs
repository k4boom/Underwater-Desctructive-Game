using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonManagerFloor : MonoBehaviour
{
    [SerializeField] private GameObject[] colons;
    void Start()
    {
        
    }

    private bool setFree = false;
    void Update()
    {
        int count = 0;
        foreach(GameObject colon in colons)
        {
            if(!colon.GetComponent<Joint>())
            {
                count++;
            }
        }
        Debug.Log("DESTRUCTED: " + GameManager.Instance.numberOfDestructed);
        if(GameManager.Instance.numberOfDestructed > 1)
        {
            setFree = true;
        }

        if(setFree)
        {
            foreach (GameObject colon in colons)
            {
                Destroy(colon.GetComponent<Joint>());
            }
            Destroy(GetComponent<Joint>());
            GetComponent<Fracture>().shouldFrac = true;
        }
    }
}
