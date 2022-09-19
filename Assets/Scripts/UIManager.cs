using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerCamera camController;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject menuUI;
    public TextMeshProUGUI leftLimit;
    public TextMeshProUGUI goldCount;
    public TextMeshProUGUI currLimit;
    public TextMeshProUGUI destructedNumber;


    public static UIManager Instance;

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
        goldCount.text = "0";
        if (PlayerPrefs.HasKey("GoldCount")) goldCount.text = PlayerPrefs.GetInt("GoldCount").ToString();
        if (PlayerPrefs.HasKey("leftLimit")) currLimit.text = "Current : " + PlayerPrefs.GetFloat("leftLimit").ToString() + "m";

    }

    private void Update()
    {

        //destructedNumber.text = GameManager.Instance.destructionPercentage.ToString();


    }



    public void StartGame()
    {

        menuUI.SetActive(false);
        inGameUI.SetActive(true);
        PlayerController.Instance.mode = PlayerController.PlayerMode.Move;
        camController.camMode = PlayerCamera.CameraMode.follow;
        GameManager.Instance.chainSpawner.gameObject.GetComponent<ChainSpawner>().StartChainSpawner();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("GoldCount",PlayerPrefs.GetInt("GoldCount")+1);
    }

    public void IncreaseRopeLimit()
    {
        PlayerPrefs.SetFloat("leftLimit", PlayerPrefs.GetFloat("leftLimit") + 2);
        currLimit.text = "Current : " + PlayerPrefs.GetFloat("leftLimit").ToString() + "m";
    }

}
