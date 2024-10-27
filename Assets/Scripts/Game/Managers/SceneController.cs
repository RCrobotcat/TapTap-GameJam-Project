using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public GameObject PlayerPrefab;
    public SceneFader SceneFaderPrefab;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleTransitionToScene("MenuScene");
        }
    }

    public void TransitionToDestination(string SceneName, TransitionDestination.DestinationTag destinationTag)
    {
        StartCoroutine(Transition(SceneName, destinationTag));
    }

    public void HandleTransitionToScene(string SceneName)
    {
        StartCoroutine(TransitionToScene(SceneName));
    }

    public void HandleContinue(string sceneName)
    {
        StartCoroutine(LoadContinueScene(sceneName));
    }

    public void HandleRespawn(string sceneName)
    {
        StartCoroutine(Respawn(sceneName));
    }

    IEnumerator Respawn(string sceneName)
    {
        SceneFader fade = Instantiate(SceneFaderPrefab);
        if (sceneName != "")
        {
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
            {
                yield return StartCoroutine(fade.FadeOut(1.2f));
                yield return SceneManager.LoadSceneAsync(sceneName);

                Vector3 PlayerPos = new Vector3(PlayerPrefs.GetFloat("PlayerX"),
                    PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
                yield return Instantiate(PlayerPrefab, PlayerPos, Quaternion.identity);
                yield return null;
                SaveManager.Instance.LoadPlayerData();
                PlayerNumController.Instance.LoadPlayerNums();
                PlayerNumController.Instance.mModel.PlayerLight.Value = PlayerNumController.Instance.currentMaxLight;

                yield return StartCoroutine(fade.FadeIn(1.2f));
                yield break;
            }
        }
    }

    IEnumerator TransitionToScene(string SceneName)
    {
        SceneFader fader = Instantiate(SceneFaderPrefab);

        if (SceneName != SceneManager.GetActiveScene().name)
        {
            yield return fader.FadeOut(1.3f);
            yield return SceneManager.LoadSceneAsync(SceneName);

            QuestManager.Instance.LoadQuestManager();
            yield return null;
            if (PlayerNumController.Instance != null)
            {
                PlayerNumController.Instance.currentMaxLight = 5.0f;
                PlayerNumController.Instance.mModel.PlayerLight.Value = PlayerNumController.Instance.currentMaxLight;
            }

            yield return fader.FadeIn(1.3f);
            yield break;
        }
    }

    IEnumerator LoadContinueScene(string sceneName)
    {
        SceneFader fade = Instantiate(SceneFaderPrefab);
        if (sceneName != "")
        {
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
            {
                yield return StartCoroutine(fade.FadeOut(1.2f));
                yield return SceneManager.LoadSceneAsync(sceneName);

                Vector3 PlayerPos = new Vector3(PlayerPrefs.GetFloat("PlayerX"),
                    PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
                yield return Instantiate(PlayerPrefab, PlayerPos, Quaternion.identity);
                yield return null;
                SaveManager.Instance.LoadPlayerData();
                PlayerNumController.Instance.LoadPlayerNums();

                yield return StartCoroutine(fade.FadeIn(1.2f));
                yield break;
            }
        }
    }

    IEnumerator Transition(string SceneName, TransitionDestination.DestinationTag destinationTag)
    {
        SaveManager.Instance.SavePlayerData();
        PlayerNumController.Instance.SavePlayerNums();

        SceneFader fader = Instantiate(SceneFaderPrefab);

        if (SceneName != SceneManager.GetActiveScene().name)
        {
            yield return fader.FadeOut(1.3f);
            yield return SceneManager.LoadSceneAsync(SceneName);

            TransitionDestination destination = GetDestination(destinationTag);
            if (destination == null)
            {
                Debug.LogError("Destination not found!");
                yield break;
            }

            yield return Instantiate(PlayerPrefab,
                destination.transform.position, Quaternion.identity);

            SaveManager.Instance.LoadPlayerData();
            PlayerNumController.Instance.LoadPlayerNums();

            SaveManager.Instance.SavePlayerData();
            SaveManager.Instance.SavePlayerPosition();
            yield return fader.FadeIn(1.3f);
            yield break;
        }
    }

    // Find the destination entrance in the new scene
    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        Debug.Log("Found " + entrances.Length + " TransitionDestinations");

        foreach (var entrance in entrances)
        {
            Debug.Log("Checking entrance with tag: " + entrance.destinationTag);
            if (entrance.destinationTag == destinationTag)
            {
                Debug.Log("Destination found: " + entrance.name);
                return entrance;
            }
        }
        Debug.LogError("No TransitionDestination found with tag: " + destinationTag);
        return null;
    }
}
