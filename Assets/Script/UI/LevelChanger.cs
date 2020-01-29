using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    public Animator animator;
    private int levelToLoad;
    private float targetValue;

    // Start is called before the first frame update
    private void Update()
    {


    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeIn");
    }
    public void OnFadeComplete()
    {
       SceneManager.LoadSceneAsync(levelToLoad);
        
    }


}
