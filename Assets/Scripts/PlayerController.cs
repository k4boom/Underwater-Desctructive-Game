using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

public class PlayerController : MonoBehaviour
{
    public enum PlayerMode { Menu, Move, DrawBack};
    public PlayerMode mode = PlayerMode.Menu;
    public GameObject player;
    public GameObject submarinee;
    public GameObject rope;
    public GameObject cam;
    [SerializeField] private float drawBackForce = 5f;
    private RopeController rp;
    public static PlayerController Instance;
    public BuildingManager bm;
    private bool allowMovement = false;
    public bool anchorCanDraw = true;
    private bool submarineCanMove = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main.gameObject;
        rp = rope.GetComponent<RopeController>(); // To make it rope seem like strecthed
    }
    void Update()
    {
        switch(mode)
        {
            case PlayerMode.Menu:
                break;
            case PlayerMode.Move:
                break;
            case PlayerMode.DrawBack:
                PullBack();
                break;
        }

        if(transform.position.z < submarinee.transform.position.z + 2.5f)
        {
            anchorCanDraw = false;
            submarineCanMove = true;
            if(!player.GetComponent<FixedJoint>())
            {
                GameManager.Instance.chainSpawner.gameObject.AddComponent<FixedJoint>().connectedBody = submarinee.GetComponent<Rigidbody>();
                player.AddComponent<FixedJoint>().connectedBody = submarinee.GetComponent<Rigidbody>();
                GameManager.Instance.MakeChainsInactive();
                //player.transform.SetParent(submarinee.transform);
            }

        }
    }

    private bool counter = false;
    void PullBack()
    {
        cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;
        if (Input.GetMouseButton(0))
        {
            counter = true;
        } else
        {
            counter = false;
            submarinee.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
    }
    private void FixedUpdate()
    {
        if (counter) PullIt();
    }

    void PullIt()
    {
        
        if (anchorCanDraw)
        {

            if (!GetComponent<Stabber>().sj)
            {
                GameManager.Instance.AddForceToChains(-Vector3.forward * 10f);
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.identity, 0.1f);
                player.GetComponent<Rigidbody>().mass = 0.01f;
                player.GetComponent<Rigidbody>().AddForce(-transform.forward * 10f);
            }
            else
            {
                drawBackForce += drawBackForce * Time.fixedDeltaTime;
                player.GetComponent<Rigidbody>().mass = 0.01f;
                player.GetComponent<Rigidbody>().AddForce(-transform.forward * drawBackForce * 2f);
            }
        } else
        {
            submarinee.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            submarinee.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 150f + Vector3.up * 100f);
        }
    }


    /// <summary>
    /// THIS IS PREVIOUS WORK
    /// </summary>

    private void DrawBack()
    {
        //player.GetComponent<PlayerMotor>().limit = 0;
        Transform firstRopeFragment = rope.transform.GetChild(0);
        Transform ropeSource = rp.ropeSource.transform;
        //Vector3 dir = new Vector3(0, 0, ropeSource.position.z - player.transform.position.z);
        Vector3 dir = ropeSource.position - player.transform.position;
        player.GetComponent<CharacterController>().Move(dir.normalized * drawBackForce * Time.deltaTime);




        /*
         * Solution 1 : Didnot work because of mass of rope
         * Destroy(player.GetComponent<PlayerMotor>());
        player.GetComponent<PlayerMotor>().limit = 0;
        Transform firstRopeFragment = rope.transform.GetChild(0);
        Transform ropeSource = rp.ropeSource.transform;
        Vector3 dir = new Vector3(0,0,ropeSource.position.z - player.transform.position.z);
        player.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log(dir);
        player.GetComponent<Rigidbody>().AddForce(dir.normalized * drawBackForce * Time.fixedDeltaTime);*/
    }

    private void DrawBackWithInput()
    {
        if(Input.GetMouseButton(0))
        {
            //submarine.MoveWithMouseInput();
            StartCoroutine("ReleaseWallWithDelay");
            if(allowMovement)   DrawBack();
        }
    }
    IEnumerator ReleaseWallWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        //bm.releaseWalls = true;
        GetComponent<Stabbing>().ChangeTag();
        GetComponent<Rigidbody>().isKinematic = false;
        //GameManager.Instance.AddTagToCollision();
        allowMovement = true;
    }
}
