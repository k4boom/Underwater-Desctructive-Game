using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabbing : MonoBehaviour
{
    [SerializeField] private string stabTag = "Stab";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(stabTag))
        {
            
            collision.transform.SetParent(transform);
            collision.rigidbody.mass = 0.01f;
            var sp = collision.gameObject.AddComponent<FixedJoint>();
            sp.connectedBody = GetComponent<Rigidbody>();
            sp.enableCollision = true;
            //SOLUTION 1
            /*
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            */
        }
    }
}
