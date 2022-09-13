using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    private float time = 0f;
    [SerializeField] private float radius = 0.2f;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float x = Mathf.Cos(time) ;
        float z = Mathf.Sin(time) ;

        transform.position += new Vector3(x, 0, z) * radius;
        transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));
    }
}
