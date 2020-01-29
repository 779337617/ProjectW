using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    LevelChanger levelChanger;
    private static UIManager instance;
    public static UIManager MyInstance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public GameObject deadHint;
    private GameManager gameManagement;
    public void OnClick(string name)
    {
        switch (name)
            {
            case "StartButton":
                //levelChanger.FadeToLevel(Random.Range(1,5));
                gameManagement.EnterNewLevel();
                break;
            case "RestartButton":
                levelChanger.FadeToLevel(0);
                Destroy(Player.MyInstance.gameObject);
                break; 

    }
}

    // Start is called before the first frame update
    void Start()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
        gameManagement = GameManager.MyInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
