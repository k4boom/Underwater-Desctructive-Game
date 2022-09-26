using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chainPrefab;
    [SerializeField] private GameObject anchor;
    [SerializeField] private GameObject chainSource;
    [SerializeField] private int numberOfChains;
    [SerializeField] private LayerMask chainLayer;
    [SerializeField] private float offset;
    [SerializeField] private Vector3 spawnOffset;
    private Camera cam;
    private HingeJoint hjoint;
    private Rigidbody rb;
    private int curr = 0;
    public List<GameObject> passiveChains;
    void Start()
    {
        numberOfChains = Mathf.RoundToInt(PlayerPrefs.GetFloat("leftLimit") * 2 / 3);
        passiveChains = new List<GameObject>();
        //Physics.IgnoreLayerCollision(chainLayer, chainLayer);
        cam = Camera.main;
        
    }

    public void StartChainSpawner()
    {
        for (int i = 0; i < numberOfChains; i++)
        {
            SpawnChains();
        }
        for (int i = 0; i < numberOfChains; i++)
        {
            SpawnChains(false);
        }
        GameManager.Instance.lastChain = passiveChains[passiveChains.Count - 1];

    }

    private int counter = 0;
    void Update()
    {
        //if(Input.GetMouseButton(0))//taçdeðiþ
        if(Input.touchCount > 0)//taçdeðiþ

        {
            counter++;
            if(counter>40)
            {
                //SpawnChainWithInput();
                //ActiveChains();
                counter = 0;
            }
        }
    }


    void ActiveChains()
    {
        if (passiveChains.Count == 0) return;
        passiveChains[0].GetComponent<MeshRenderer>().enabled = true;
        //SetActive(true);
        passiveChains.RemoveAt(0);
    }

    void SpawnChainWithInput()
    {
        GameObject chain = Instantiate(chainPrefab, firstChain.transform.position, Quaternion.Euler(0, 90, 90), transform);
        chain.transform.Rotate(new Vector3(0, 90, 0) * curr);
        firstChain.GetComponent<HingeJoint>().connectedBody = chain.GetComponentInChildren<Rigidbody>();
        hjoint = chain.GetComponentInChildren<HingeJoint>();
        hjoint.connectedBody = chainSource.GetComponent<Rigidbody>();
        firstChain = chain;
    }

    private GameObject firstChain;
    void SpawnChainsFromSource()
    {
        GameObject chain = Instantiate(chainPrefab, transform.position + (curr * offset) * DirectionHandler(), Quaternion.Euler(0, 90, 90), transform);
        chain.transform.Rotate(new Vector3(0, 90, 0) * curr);

        if (curr == 0)
        {
            hjoint = chain.GetComponentInChildren<HingeJoint>();
            hjoint.connectedBody = chainSource.GetComponent<Rigidbody>();
            rb = chain.GetComponentInChildren<Rigidbody>();
            firstChain = chain;
        }
        else
        {
            hjoint = chain.GetComponentInChildren<HingeJoint>();
            hjoint.connectedBody = rb;
            rb = chain.GetComponentInChildren<Rigidbody>();
        }

        if (curr == numberOfChains - 1)
        {
            hjoint = chain.AddComponent<HingeJoint>();
            hjoint.connectedBody = anchor.GetComponent<Rigidbody>();
        }

        curr++;
    }

    void SpawnChains(bool active = true)
    {
        GameObject chain = Instantiate(chainPrefab, transform.position + spawnOffset - (curr * offset) * DirectionHandler(), Quaternion.Euler(0,90,90), transform); 
        chain.transform.Rotate(new Vector3(0, 90, 0) * curr);
        chain.GetComponent<ChainFragment>().renderPoint = chainSource.transform;
        if (curr == 0)
        {
            //Destroy(chain.GetComponentInChildren<HingeJoint>());
            hjoint = chain.GetComponentInChildren<HingeJoint>();
            hjoint.connectedBody = anchor.GetComponent<Rigidbody>();
            //hjoint.anchor = new Vector3(0, -0.03f, 0);
            rb = chain.GetComponentInChildren<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            //rb.useGravity = false;
            //rb.isKinematic = true;
            //chain.AddComponent<MouseInputMovement>();
            //CameraFollow cm = cam.gameObject.AddComponent<CameraFollow>();
            //cm.player = chain.transform;
        }
        else
        {
            hjoint = chain.GetComponentInChildren<HingeJoint>();
            hjoint.connectedBody = rb;
            rb = chain.GetComponentInChildren<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if(curr == numberOfChains-1)
        {
            //hjoint = chain.AddComponent<HingeJoint>();
            //hjoint.connectedBody = chainSource.GetComponent<Rigidbody>();
        }
        chain.GetComponent<MeshRenderer>().enabled = active;
            //SetActive(active);
        if (!active)
        {
            passiveChains.Add(chain);
        }
        curr++;
    }

    [SerializeField] private transformDirection movementDirection = transformDirection.forward;
    private enum transformDirection
    { forward, right, up };
    private Vector3 DirectionHandler()
    {
        Vector3 spawnDir = transform.forward;
        switch (movementDirection)
        {
            case transformDirection.forward:
                spawnDir = transform.forward;
                break;
            case transformDirection.right:
                spawnDir = transform.right;
                break;
            case transformDirection.up:
                spawnDir = transform.up;
                break;
            default:
                break;
        }
        return spawnDir;
    }
}
