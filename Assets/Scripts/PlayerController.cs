using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

public class PlayerController : MonoBehaviour
{
    public enum PlayerMode { Menu, Move, DrawBack};
    public PlayerMode mode = PlayerMode.Move;
    public GameObject player;
    public GameObject rope;
    public GameObject cam;
    [SerializeField] private float drawBackForce = 5f;
    private RopeController rp;
    public static PlayerController Instance;
    public BuildingManager bm;
    private bool allowMovement = false;
    public SubmarineMovement submarine;
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
                DrawBackWithInput();
                break;
        }
    }

    

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
            submarine.MoveWithMouseInput();
            StartCoroutine("ReleaseWallWithDelay");
            if(allowMovement)   DrawBack();
        }
    }
    IEnumerator ReleaseWallWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        bm.releaseWalls = true;
        allowMovement = true;
    }
}
