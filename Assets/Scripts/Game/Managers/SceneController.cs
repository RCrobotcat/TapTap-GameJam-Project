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
        if (SceneName != SceneManager.GetActiveScene().name)
        {
            yield return SceneManager.LoadSceneAsync(SceneName);
            yield break; // Exit the coroutine
        }
    }
}
