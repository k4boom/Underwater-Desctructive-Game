using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerCamera camController;
    [SerializeField] private GameObject[] inGameUI;
    [SerializeField] private GameObject[] menuUI;
    [SerializeField] private GameObject[] beforeEndGameUI;
    [SerializeField] private GameObject[] endGameUI;
    [SerializeField] private GameObject[] stars;
    public TextMeshProUGUI leftLimit;
    public TextMeshProUGUI goldCount;
    public TextMeshProUGUI currLimit;
    public TextMeshProUGUI destructedNumber;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI levelCompleteButtonText;


    private static UIManager _Instance;
    public static UIManager Instance { get { return _Instance; } }

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
        /*
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }*/
        /*
        DontDestroyOnLoad(gameObject);
        Instance = this;*/
    }

    private void Start()
    {
        goldCount.text = "0";
        if (PlayerPrefs.HasKey("GoldCount")) goldCount.text = PlayerPrefs.GetInt("GoldCount").ToString();
        if (PlayerPrefs.HasKey("leftLimit"))
        {
            currLimit.text = "Current : " + PlayerPrefs.GetFloat("leftLimit").ToString() + "m";
        } else
        {
            PlayerPrefs.SetFloat("leftLimit", 100f);
        }

    }

    private void Update()
    {

        //destructedNumber.text = GameManager.Instance.destructionPercentage.ToString();


    }

    void ActivateArrayElements(GameObject[] arr, bool param = true)
    {
        foreach(GameObject element in arr)
        {
            element.SetActive(param);
        }
    }

    public void StartGame()
    {
        ActivateArrayElements(menuUI, false);
        ActivateArrayElements(inGameUI, true);
        //menuUI.SetActive(false);
        //inGameUI.SetActive(true);
        PlayerController.Instance.mode = PlayerController.PlayerMode.Move;
        camController.camMode = PlayerCamera.CameraMode.follow;
        GameManager.Instance.chainSpawner.gameObject.GetComponent<ChainSpawner>().StartChainSpawner();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.numberOfDestructed = 0;
        PlayerPrefs.SetInt("GoldCount",PlayerPrefs.GetInt("GoldCount")+1);
    }

    public void IncreaseRopeLimit()
    {
        PlayerPrefs.SetFloat("leftLimit", PlayerPrefs.GetFloat("leftLimit") + 2);
        currLimit.text = "Current : " + PlayerPrefs.GetFloat("leftLimit").ToString() + "m";
    }

    public void BringBeforeEndGameUI()
    {
        ActivateArrayElements(beforeEndGameUI, true);
    }

    public void BringEndGameUI()
    {
        ActivateArrayElements(inGameUI, false);
        ActivateArrayElements(beforeEndGameUI, false);
        ActivateArrayElements(endGameUI, true);
        CalculateStars();
    }
    private bool result = false;
    private void CalculateStars()
    {
        Debug.Log(GameManager.Instance.destructionPercentage);
        levelCompleteText.text = "LEVEL COMPLETED";
        levelCompleteButtonText.text = "NEXT";
        result = true;
        if (GameManager.Instance.destructionPercentage > 0.75f)
        {
            //3 Star
            ActivateStars(3);
        } else if (GameManager.Instance.destructionPercentage > 0.50f)
        {
            //2 Star
            ActivateStars(2);
        } else if (GameManager.Instance.destructionPercentage > 0.25f)
        {
            //1 Star
            ActivateStars();
        } else
        {
            levelCompleteButtonText.text = "AGAIN";
            levelCompleteText.text = "LEVEL FAILED";
            result = false;
        }
    }

    private void ActivateStars(int number = 1)
    {
        for(int i=0; i < number; i++)
        {
            stars[i].SetActive(true);
            //animation
        }
    }

    public void GameResultAction()
    {
        if(result)
        {
            Debug.Log("NEXT LEVEL");
            NextLevel();
        } else
        {
            Debug.Log("RESTART");

            RestartGame();
        }
    }

    public void NextLevel()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneNo >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene((int)Random.Range(0, SceneManager.sceneCountInBuildSettings -1));
        } else
        {
            SceneManager.LoadScene(sceneNo);
        }
    }

}
