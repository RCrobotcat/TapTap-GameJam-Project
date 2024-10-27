using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public GameObject door;

    void Update()
    {
        if (QuestManager.Instance.questTasks.Count > 0 && !isAllInventoryItemsNull())
        {
            bool isCorrectQuest = QuestManager.Instance.questTasks[0].questData.name == "LustElimination(Clone)";
            bool isCorrectItem = InventoryManager.Instance.inventoryData.items[0].itemData.name == "PlayerBasic";

            if (isCorrectQuest && isCorrectItem)
            {
                door.SetActive(true);
            }
        }
        else if (QuestManager.Instance.questTasks.Count > 0 && isAllInventoryItemsNull()
            && InventoryManager.Instance.actionData.items[0].itemData != null)
        {
            bool isCorrectQuest = QuestManager.Instance.questTasks[0].questData.name == "LustElimination(Clone)";
            bool isCorrectItem = InventoryManager.Instance.actionData.items[0].itemData.name == "PlayerBasic";

            if (isCorrectQuest && isCorrectItem)
            {
                door.SetActive(true);
            }
        }
    }

    public bool isAllInventoryItemsNull()
    {
        foreach (var item in InventoryManager.Instance.inventoryData.items)
        {
            if (item.itemData != null)
            {
                return false;
            }
        }
        return true;
    }
}
