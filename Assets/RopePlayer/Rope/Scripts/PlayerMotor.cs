using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;

    [SerializeField]  private float baseSpeed = 10.0f;
    [SerializeField]  private float rotSpeedX = 3.0f;
    [SerializeField]  private float rotSpeedY = 1.5f;
    [SerializeField] private Vector2 inputs;
    [SerializeField] private bool isLimited;
    [SerializeField] public float limit = 10.0f;
    [SerializeField] private float finishSpeed = 1.0f;
    
    private float currentLimit;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("leftLimit"))
            limit = PlayerPrefs.GetFloat("leftLimit");
        else
            PlayerPrefs.SetFloat("leftLimit", limit);
        currentLimit = limit;
    }

    private void Update()
    {
        //UIManager.Instance.leftLimit.text = currentLimit.ToString() + " m";

        if(Input.GetMouseButton(0) && PlayerController.Instance.mode == PlayerController.PlayerMode.Move)
        {
            if (isLimited)
                MoveWithLimit();
            else
                Move();
        }
    }
    
    private void MoveWithLimit()
    {
        if(currentLimit > 0)
        {
            Move();
        }
        currentLimit -= finishSpeed * Time.deltaTime;
    }
    private void Move()
    {
        Vector3 moveVector = transform.forward * baseSpeed;


        //TODO: NEED TO FIND THAT AN ALTERNATIVE
        //Vector3 inputs = Manager.Instance.GetPlayerInput();
        var ver = Input.GetAxis("Mouse X");
        var hor = Input.GetAxis("Mouse Y");
        inputs = new Vector2(ver, hor);


        Vector3 yaw = inputs.x * transform.right * rotSpeedX;// * Time.deltaTime;
        Vector3 pitch = inputs.y * transform.up * rotSpeedY;//* Time.deltaTime;
        Vector3 dir = yaw + pitch;

        float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;

        if(maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
        {

        } else
        {
            moveVector += dir;

            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        
        controller.Move(moveVector * Time.deltaTime); 
    }

}
