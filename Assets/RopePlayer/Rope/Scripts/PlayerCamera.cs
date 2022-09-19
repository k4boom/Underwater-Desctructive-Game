using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform building;
    public Transform submarine;
    public Transform drawPosition;
    private Vector3 desiredPosition;
    [SerializeField] private Vector3 drawOffset;
    [SerializeField] private float drawSpeed = 1.0f;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private float distance = 3.5f;
    
    public enum CameraMode { menu, follow, draw };
    public CameraMode camMode = CameraMode.menu;

    private void Start()
    {
        //drawOffset = new Vector3(0, 5, -6);
    }

    private void Update()
    {
        switch(camMode)
        {
            case CameraMode.menu:

                break;
            case CameraMode.follow:
                FollowPlayer();
                break;
            case CameraMode.draw:
                DrawCameraBack();
                break;
        }   
    }

    void FollowPlayer()
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

    public void DrawCameraBack()
    {
        desiredPosition = drawPosition.position + drawOffset;
        Vector3 dir = desiredPosition - transform.position;
        transform.position += dir * drawSpeed * Time.deltaTime;
        Vector3 diff = submarine.position - building.position;
        Vector3 drawLookAtPosition = building.position + (diff / 2);
        transform.LookAt(drawLookAtPosition + (transform.up * offset));
        
    }

}
