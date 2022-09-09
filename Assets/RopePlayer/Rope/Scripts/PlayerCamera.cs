using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform lookAt;

    private Vector3 desiredPosition;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private float distance = 3.5f;
    [SerializeField] float necessaryDistance = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Camera pos: " + transform.position + " Player Pos: " + lookAt.position + " Desired Pos : " + desiredPosition);
        Vector2 player = new Vector2(lookAt.position.x, lookAt.position.z);
        Vector2 cam = new Vector2(transform.position.x, transform.position.z);

        if (true)//Vector2.Distance(player, cam) > necessaryDistance)
        {
            //update position
            desiredPosition = lookAt.position + (-transform.forward * distance) + (transform.up * offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.05f);
            //update rotation
            transform.LookAt(lookAt.position + (transform.up * offset));
        } else
        {
            //transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0.5f, 0, 0.5f), 0.05f);
        }
    
    }
}
