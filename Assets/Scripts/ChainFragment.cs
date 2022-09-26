using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainFragment : MonoBehaviour
{
    [SerializeField] public Transform renderPoint;
   
    void Update()
    {
        if(renderPoint.position.z - 0.5f < transform.position.z && PlayerController.Instance.anchorCanDraw)
        {
            GetComponent<Renderer>().enabled = true;
        } else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
