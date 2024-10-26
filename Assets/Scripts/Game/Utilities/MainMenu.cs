using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Delete all saved data
        PlayerPrefs.DeleteAll();

        SceneController.Instance.HandleTransitionToScene("TutorialScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
