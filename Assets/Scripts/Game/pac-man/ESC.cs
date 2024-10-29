using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    public GameObject MenuList;
    private bool menuKeys = true;
    public AudioSource bgm;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene")
        {
            if (menuKeys)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    MenuList.SetActive(true);
                    menuKeys = false;
                    // Time.timeScale = 0;
                    // bgm.Pause();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuList.SetActive(false);
                menuKeys = true;
                // Time.timeScale = 1;
                // bgm.Play();
            }
        }
    }

    public void ReturnToMain()
    {
        /*if (!isdead)
        {
            MenuList.SetActive(false);
            menuKeys = true;
            Time.timeScale = 1;
            bgm.Play();
        }*/
        SceneController.Instance.HandleTransitionToScene("MenuScene");
    }

    public void LoadLatestSaving()
    {
        /*isdead = false;
        SceneManager.LoadScene(0);//场景
        Time.timeScale = 1;*/
        SceneController.Instance.HandleContinue(SaveManager.Instance.SceneName);
    }

    public void Exit()//退出游戏
    {
        Application.Quit();
    }

    public void Window()
    {
        // 确保窗口化模式生效
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1280, 720, false);
    }

    public void FullScreen()
    {
        // 获取所有分辨率并设置为全屏的最高分辨率
        Resolution[] resolutions = Screen.resolutions;
        Resolution highestResolution = resolutions[resolutions.Length - 1];

        Screen.SetResolution(highestResolution.width, highestResolution.height, true);
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; // 确保是独占全屏模式
    }
}
