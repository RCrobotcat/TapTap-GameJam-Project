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
    public Transform playerTransform;   //��ɫҪ�л�������
    public GameSceneSO currentScene;    //��ǰ����
    private  GameSceneSO sceneToLoad;     //Ҫת���ĳ���
    public  Vector3 positionToGo;        //Ҫȥ��������
    private  bool fadeScreen;             //�Ƿ��뽥��

    public GameSceneSO fristScene;     //��һ��Ҫ���صĳ���
    public Vector3 fristPosition;

    [Header("�¼�����")]
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

        if (currentScene != null)       //�����ǰ������Ϊ�գ�ж���������볡��
        {
            StartCoroutine(UnLoadPreviousScene()); //��Я������ж�����ɳ���
        }
        else                            // ���볡��
        {
            LoadNewScene(); 
        }
        
    }

    private IEnumerator UnLoadPreviousScene()
    {
        //TODD:���뽥��

        // loadEventSO.RaiseLoadRequestEvent(sceneToLoad,positionToGo,true);
        yield return currentScene.sceneReference.UnLoadScene();  //ж�ص�ǰ����
        playerTransform.gameObject.SetActive(false);
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        Debug.Log(1);
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        //loadingOption.Completed += OnLoadCompleted;             //�ڳ���������ɺ�ִ�з�
        currentScene = sceneToLoad;
        playerTransform.position = positionToGo;
        playerTransform.gameObject.SetActive(true);
    }
}
