using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainFrag : MonoBehaviour
{
    private Joint joint;
    void Start()
    {
        joint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Currentforce: " + joint.currentForce + " CurrentTorque" + joint.currentTorque);
    }
}
