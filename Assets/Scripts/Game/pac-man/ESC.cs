using UnityEngine;

public class ESC : MonoBehaviour
{
    public GameObject MenuList;
    private bool menuKeys = true;
    public AudioSource bgm;

    void Update()
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
        Screen.SetResolution(1280, 720, false);
    }

    public void full_screen()
    {
        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        Screen.fullScreen = true;
    }
}
