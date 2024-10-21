using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    string sceneName = "";

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SavePlayerData();
        }
    }

    public void SavePlayerData()
    {
        // Save(GameManager.Instance.playerStatus.characterData, GameManager.Instance.playerStatus.characterData.name);
        InventoryManager.Instance.SaveData();
        QuestManager.Instance.SaveQuestManager();
    }

    public void LoadPlayerData()
    {
        // Load(GameManager.Instance.playerStatus.characterData, GameManager.Instance.playerStatus.characterData.name);
        InventoryManager.Instance.LoadData();
        QuestManager.Instance.LoadQuestManager();
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            PlayerController.Instance.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }
    }

    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.SetFloat("PlayerX", PlayerController.Instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerController.Instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", PlayerController.Instance.transform.position.z);
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