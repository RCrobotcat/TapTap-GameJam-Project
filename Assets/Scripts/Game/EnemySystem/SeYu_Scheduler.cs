using UnityEngine;

public class SeYu_Scheduler : MonoBehaviour
{
    public GameObject SeYu_Sheep;
    public GameObject SeYu_Rabbit_final;

    public QuestData_SO SeYuQuest;

    public bool SeYu_Dead;

    void Update()
    {
        if (!SeYu_Sheep.activeSelf && !SeYu_Rabbit_final.activeSelf && !QuestManager.Instance.GetQuestTask(SeYuQuest).IsCompleted)
        {
            SeYu_Rabbit_final.SetActive(true);
            PlayerNumController.Instance.mModel.PlayerLight.Value = PlayerNumController.Instance.currentMaxLight;
        }

        if (!SeYu_Rabbit_final.activeSelf && QuestManager.Instance.GetQuestTask(SeYuQuest).IsCompleted)
        {
            SeYu_Dead = true;
        }

        // Already Defeated Lust
        if (SeYu_Dead)
        {
            SeYu_Sheep.SetActive(false);
            SeYu_Rabbit_final.SetActive(false);
            InventoryManager.Instance.EnemyHealthPanel.SetActive(false);
        }
    }
}
