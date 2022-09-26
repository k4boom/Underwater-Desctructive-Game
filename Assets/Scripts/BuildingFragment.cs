using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFragment : MonoBehaviour
{
    private Joint joint;
    void Start()
    {
        joint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!joint) //&& !gameObject.GetComponent<Fracture>())
        {
            Fracture frac = gameObject.GetComponent<Fracture>();
            frac.isEnabled = true; 
            
        }
    }
}
