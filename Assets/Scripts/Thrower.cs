using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private GameObject throwObject;
    [SerializeField] private float forceMagnitude = 10f;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

    Ray ray;
    private void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);


        if (Input.GetMouseButtonDown(0))
        {
            ObjectShoot();
        }
    }

    private void ObjectShoot()
    {
        var ball = Instantiate(throwObject, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(ray.direction * 50 * forceMagnitude);
    }
}
