using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithPhysics : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float baseSpeed = 10.0f;
    [SerializeField] private float rotSpeedX = 3.0f;
    [SerializeField] private float rotSpeedY = 1.5f;
    [SerializeField] private Vector2 inputs;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject chainSource;
    private float limit;
    private float currentLimit;
    private Vector3 moveVector;
    private List<Rigidbody> chains;
    void Start()
    {
        limit = PlayerPrefs.GetFloat("leftLimit");
        currentLimit = limit;
        rb = GetComponent<Rigidbody>();
        chains = new List<Rigidbody>();
    }

    private bool allowedToMove = false;
    void Update()
    {
        if(currentLimit > 0)    UIManager.Instance.leftLimit.text = currentLimit.ToString("0.00") + " m";

        if (chains.Count < 1 && GameManager.Instance.chains.Count > 0)
        {
            foreach (Transform chain in chainSource.transform)
            {
                chains.Add(chain.gameObject.GetComponent<Rigidbody>());
            }
        }

        if(currentLimit < 0)
        {
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
        }

        if (Input.touchCount > 0 && PlayerController.Instance.mode == PlayerController.PlayerMode.Move && GameManager.Instance.anchorCanMove && currentLimit > 0)
        {
            allowedToMove = true;
            foreach (Rigidbody chain in chains)
            {
                chain.isKinematic = false;
            }
            Move();
        } else
        {
            allowedToMove = false;
            rb.velocity = Vector3.zero;

            
            rb.angularVelocity = Vector3.zero;
            foreach(Rigidbody chain in chains)
            {
                chain.velocity = Vector3.zero;


                chain.angularVelocity = Vector3.zero;
            }
            
        }


    }


    private void FixedUpdate()
    {
        if(allowedToMove) Fly();
    }


    void Fly()
    {
        rb.velocity = moveVector * moveSpeed;
    }
    private void Move()
    {
        currentLimit -= 0.1f;
        moveVector = transform.forward * baseSpeed;


        Vector3 inputs = InputManager.Instance.GetPlayerInput();


        Vector3 yaw = inputs.x * transform.right * rotSpeedX * Time.deltaTime;
        Vector3 pitch = inputs.y * transform.up * rotSpeedY * Time.deltaTime;
        Vector3 dir = yaw + pitch;

        float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;

        if (maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
        {

        }
        else
        {
            moveVector += dir;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), 0.2f);
        }


    }

}
