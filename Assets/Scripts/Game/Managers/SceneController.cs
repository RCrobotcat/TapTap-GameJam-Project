using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public IEnumerator Transition(string SceneName)
    {
        SaveManager.Instance.SavePlayerData();
        PlayerNumController.Instance.SavePlayerNums();
        if (SceneName != SceneManager.GetActiveScene().name)
        {
            yield return SceneManager.LoadSceneAsync(SceneName);
            SaveManager.Instance.LoadPlayerData();
            PlayerNumController.Instance.LoadPlayerNums();
            yield break;
        }
    }
}
