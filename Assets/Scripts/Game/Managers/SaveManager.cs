using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    string sceneName = "currentScene";

    public string SceneName
    {
        get
        {
            return PlayerPrefs.GetString(sceneName);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SavePlayerData();
            SavePlayerPosition();
            QuestUI.Instance.SaveCompletedText.SetActive(true);

            // Set the QuestCompletedText to inactive after 2 seconds
            StartCoroutine(DeactivateSaveCompletedText());
        }

        // For Saving Logic Testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayerData();
            LoadPlayerPosition();
        }
    }

    public IEnumerator DeactivateSaveCompletedText()
    {
        yield return new WaitForSeconds(2f);
        QuestUI.Instance.SaveCompletedText.SetActive(false);
    }

    public void SavePlayerData()
    {
        // Save(GameManager.Instance.playerStatus.characterData, GameManager.Instance.playerStatus.characterData.name);
        InventoryManager.Instance.SaveData();
        PlayerNumController.Instance.SavePlayerNums();
        QuestManager.Instance.SaveQuestManager();
    }

    public void LoadPlayerData()
    {
        // Load(GameManager.Instance.playerStatus.characterData, GameManager.Instance.playerStatus.characterData.name);
        InventoryManager.Instance.LoadData();
        PlayerNumController.Instance.LoadPlayerNums();
        QuestManager.Instance.LoadQuestManager();
    }

    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", PlayerController.Instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerController.Instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", PlayerController.Instance.transform.position.z);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Vector3 savedPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );

            // Ensure the NavMeshAgent component is referenced
            NavMeshAgent agent = PlayerController.Instance.agent;

            if (agent != null)
            {
                // Use Warp to move the agent to the saved position
                agent.Warp(savedPosition);
            }
            else
            {
                // If there's no NavMeshAgent, set the position directly
                PlayerController.Instance.transform.position = savedPosition;
            }
        }
    }

    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.DeleteKey(sceneName); // delete scene name
        PlayerPrefs.SetString(sceneName, SceneManager.GetActiveScene().name); // save scene name
        PlayerPrefs.Save(); // save to disk

        Debug.Log("Data saved! ");
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}