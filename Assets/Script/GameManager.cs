using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private DDASystem DDASystem;
    private EnemyGenerator enemyGenerator;
    private LevelController levelController;
    private Player player;
    public GameObject playerPrefeb;

    private static GameManager instance;
    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

 



    private void Awake()
    {      
        DDASystem = GetComponent<DDASystem>();
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        player = Player.MyInstance;
        levelController = GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "StartScene" && SceneManager.GetActiveScene().name != "LoadingScene")
        {
            if (player == null)
            {
                GameObject g  =  Instantiate(playerPrefeb);
                g.SetActive(true);
                player = Player.MyInstance;
                levelController.enabled = true;
                levelController.player = player;
                levelController.GenerateNewLevel();
                

            }
        }
       

    }



    /// <summary> 
    /// 进入新关卡，调用levelcontroller
    /// 
    /// </summary>
    public void EnterNewLevel()
    {
        if(player == null)
        {
            LevelSwitch(1);     
        }
        else
        {
            player.LevelTag++;

        }

       //levelController.GenerateNewLevel();
    }


    /// <summary>
    /// 切换关卡场景
    /// </summary>
    public void LevelSwitch(int sceneID)
    {
        LevelChanger levelChanger = FindObjectOfType<LevelChanger>();
        levelChanger.FadeToLevel(sceneID);
    }
}
