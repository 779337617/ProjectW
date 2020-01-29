using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    public Text loadingText;

    private float loadingSpeed = 1;

    private float targetValue;

    private AsyncOperation operation;

    // Use this for initialization
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(2);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }

    // Update is called once per frame
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }



        loadingText.text = ((int)(targetValue * 100)).ToString() + "%";

        if ((int)(targetValue * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
    }


}
