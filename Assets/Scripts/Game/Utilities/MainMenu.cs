using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public ItemData_SO basicWeapon;
    public ItemData_SO rageWeapon;

    public void NewGame()
    {
        // Delete all saved data
        PlayerPrefs.DeleteAll();

        // Reset the picked status of the weapons
        basicWeapon.isPicked = false;
        rageWeapon.isPicked = false;

        SceneController.Instance.HandleTransitionToScene("TutorialScene");
    }

    public void ContinueGame()
    {
        if (SaveManager.Instance.SceneName != "")
            SceneController.Instance.HandleContinue(SaveManager.Instance.SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
