using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    public GameObject PlayerPrefab;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void TransitionToDestination(string SceneName, TransitionDestination.DestinationTag destinationTag)
    {
        StartCoroutine(Transition(SceneName, destinationTag));
    }

    IEnumerator Transition(string SceneName, TransitionDestination.DestinationTag destinationTag)
    {
        SaveManager.Instance.SavePlayerData();
        PlayerNumController.Instance.SavePlayerNums();

        if (SceneName != SceneManager.GetActiveScene().name)
        {
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
