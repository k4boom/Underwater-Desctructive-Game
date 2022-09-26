using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    public GameObject lastChain;
    public Transform chainSpawner;
    public ChainSpawner cS;
    public Transform chainSource;
    public List<GameObject> chains;
    private static GameManager _Instance;

    public int numberOfDestructed = 0;
    private int totalNumberOfBuildingParts;
    public float destructionPercentage = 0f;
    public Transform buildingParent;
    public static GameManager Instance { get { return _Instance; } }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
        }
        Application.targetFrameRate = 60;
    }
    
    private void Start()
    {
        totalNumberOfBuildingParts = buildingParent.childCount - 4;
    }

    public bool anchorCanMove = true;

    private void Update()
    {
        if(chains.Count < 1)
        {
            GameObject[] arr = GameObject.FindGameObjectsWithTag("Chain");
            chains = new List<GameObject>();
            for (int i = 0; i < arr.Length; i++)
            {
                chains.Add(arr[i]);
            }
        }
        

        if (lastChain && lastChain.transform.position.z > chainSource.position.z - 0.5f)
        {
            anchorCanMove = false;
            PlayerController.Instance.mode = PlayerController.PlayerMode.DrawBack;
        }
    }

    public void AddForceToChains(Vector3 force)
    {
        Vector3 offset = force / 160;
        int current = 0;
        foreach(GameObject chain in chains)
        {
            chain.GetComponent<Rigidbody>().AddForce(force/5 + (current++ * offset));
        }
    }

    public void MakeChainsKinematic()
    {
        
        foreach (GameObject chain in chains)
        {
            chain.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void MakeChainsInvisible()
    {
        MakeChainsKinematic();
        foreach (GameObject chain in chains)
        {
            chain.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void MakeChainsInactive()
    {
        MakeChainsKinematic();
        foreach (GameObject chain in chains)
        {
            chain.SetActive(false);
        }
    }


    public void ReportDestructed()
    {
        //Debug.Log((float)++numberOfDestructed/(float)totalNumberOfBuildingParts);
        destructionPercentage = (float)++numberOfDestructed / (float)totalNumberOfBuildingParts;
    }


}
