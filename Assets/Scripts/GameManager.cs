using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public GameObject lastChain;
    public Transform chainSpawner;
    public ChainSpawner cS;
    public Transform chainSource;
    public List<GameObject> chains;
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
        
        /*
        foreach(Transform child in chainSpawner.childCount)
        {
            chains.Add(child);
        }*/
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
        

        if (lastChain.transform.position.z > chainSource.position.z - 0.5f)
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

    /*
    private static int numberOfDestructed;
    private bool settedFree = false;
    [SerializeField] private GameObject[] ceilingBlocks;
    [SerializeField] private GameObject[] buildingBlocks;
    [SerializeField]private List<GameObject> ceilingFragments;
    [SerializeField] private List<GameObject> buildingFragments;
    private void Start()
    {
        buildingFragments = new List<GameObject>();
        ceilingFragments = new List<GameObject>();
        foreach(GameObject block in ceilingBlocks)
        {
            foreach(Transform child in  block.transform)
            {
                ceilingFragments.Add(child.gameObject);
            }
        }

        foreach (GameObject block in buildingBlocks)
        {
            foreach (Transform child in block.transform)
            {
                buildingFragments.Add(child.gameObject);
            }
        }


    }

    private bool tagsAreAdded = false;
    public void AddTagToCollision()
    {
        if(!tagsAreAdded){
            tagsAreAdded = true;
            foreach (GameObject fragment in buildingFragments)
            {
                fragment.GetComponent<UnfreezeFragment>().triggerOptions.triggerAllowedTags.Add("DestroyableObject");
                fragment.GetComponent<UnfreezeFragment>().triggerOptions.triggerAllowedTags.Add("Rope");
            }
        }
    }


    public void SetFree()
    {
        Debug.Log(numberOfDestructed++);
        if(numberOfDestructed > 1 && !settedFree)
        {
            foreach(GameObject frag in ceilingFragments)
            {
                frag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

    private int numberOfDestructedFrags = 0;
    public float destructionPercentage = 0f;
    public void IncreaseDestructedNumber()
    {
        numberOfDestructedFrags++;
        destructionPercentage = numberOfDestructedFrags / (float)buildingBlocks.Length;
    }*/
}
