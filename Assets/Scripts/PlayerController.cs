using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;
using Obi;

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
    public bool canBreak = false;
    public ChainExtender cursor;

    public GameObject cube;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    private bool firstDraw = false;
    public bool beforeDraw = false;
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
                if (!firstDraw) StartCoroutine("BeforeDraw");
                PullBack();
                break;
        }

        if(transform.position.z < submarinee.transform.position.z + 2.5f)
        {
            anchorCanDraw = false;
            submarineCanMove = true;
            if(!player.GetComponent<FixedJoint>())
            {
                StartCoroutine("EndGameUI");
                GameManager.Instance.chainSpawner.gameObject.AddComponent<FixedJoint>().connectedBody = submarinee.GetComponent<Rigidbody>();
                player.AddComponent<FixedJoint>().connectedBody = submarinee.GetComponent<Rigidbody>();
                GameManager.Instance.MakeChainsInactive();
                //player.transform.SetParent(submarinee.transform);
            }

        }
    }

    IEnumerator BeforeDraw()
    {
        firstDraw = true;
        beforeDraw = true;
        yield return new WaitForSeconds(0.5f);
        cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;
        yield return new WaitForSeconds(1f);
        yield return null;

    }

    IEnumerator EndGameUI()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.BringBeforeEndGameUI();
    }

    private bool counter = false;
    void PullBack()
    {
        Debug.Log(beforeDraw);
        if(!beforeDraw) cam.GetComponent<PlayerCamera>().camMode = PlayerCamera.CameraMode.draw;
        if (Input.touchCount > 0)
        {
            beforeDraw = true;

            counter = true;
        } else
        {
            counter = false;
            submarinee.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
    }
    private void FixedUpdate()
    {
        //if (counter) PullIt(); // This is for joint based chain
        if (counter)
        {
            PullRope();

        } else
        {
            cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public IEnumerator ChangeCanBreak()
    {
        yield return new WaitForSeconds(0f);
        canBreak = true;
    }

    void DrawCubeBack()
    {
        //cube.GetComponent<Rigidbody>().velocity = (-transform.forward * drawBackForce);
        cube.GetComponent<Rigidbody>().AddForce(-Vector3.forward * drawBackForce );
    }


    void PullRope()
    {

        if (anchorCanDraw)
        {

            if (!GetComponent<Stabber>().sj)
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.identity, 0.1f);
                //cursor.DecreaseLength();
                DrawCubeBack();
                //player.GetComponent<Rigidbody>().AddForce(-transform.forward * 10f);
            }
            else
            {
                //cursor.DecreaseLength();
                DrawCubeBack();
                /*
                drawBackForce += drawBackForce * Time.fixedDeltaTime;
                player.GetComponent<Rigidbody>().mass = 0.01f;
                player.GetComponent<Rigidbody>().AddForce(-transform.forward * drawBackForce * 2f);
                */
                }
        }
        else
        {
            submarinee.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            submarinee.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 150f + Vector3.up * 100f);
        }
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


}
