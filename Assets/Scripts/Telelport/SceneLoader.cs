using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
//using UnityEngine.ResourceManagement.ResourceProviders;


public class SceneLoader : MonoBehaviour
{
    public Transform playerTransform;   //角色要切换的坐标
    public GameSceneSO currentScene;    //当前场景
    private  GameSceneSO sceneToLoad;     //要转换的场景
    public  Vector3 positionToGo;        //要去往的坐标
    private  bool fadeScreen;             //是否渐入渐出

    public GameSceneSO fristScene;     //第一个要加载的场景
    public Vector3 fristPosition;

    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;

    private void Start()
    {
        loadEventSO.RaiseLoadRequestEvent(fristScene, fristPosition,true);
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }
    private void OnLoadRequestEvent(GameSceneSO sceneToLoad_TMP, Vector3 positionToGo_TMP, bool fadeScreen_TMP)
    {
        sceneToLoad = sceneToLoad_TMP;
        positionToGo = positionToGo_TMP;
        fadeScreen = fadeScreen_TMP;

        if (currentScene != null)       //如果当前场景不为空，卸载重新载入场景
        {
            StartCoroutine(UnLoadPreviousScene()); //此携程用于卸载生成场景
        }
        else                            // 载入场景
        {
            LoadNewScene(); 
        }
        
    }

    private IEnumerator UnLoadPreviousScene()
    {
        //TODD:渐入渐出

        // loadEventSO.RaiseLoadRequestEvent(sceneToLoad,positionToGo,true);
        yield return currentScene.sceneReference.UnLoadScene();  //卸载当前场景
        playerTransform.gameObject.SetActive(false);
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        Debug.Log(1);
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        //loadingOption.Completed += OnLoadCompleted;             //在场景加载完成后执行方
        currentScene = sceneToLoad;
        playerTransform.position = positionToGo;
        playerTransform.gameObject.SetActive(true);
    }
}
