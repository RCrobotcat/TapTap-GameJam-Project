using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public GameObject door;

    void Update()
    {
        if (QuestManager.Instance.questTasks.Count > 0
            && InventoryManager.Instance.inventoryData.items[0].itemData != null)
        {
            if (QuestManager.Instance.questTasks[0].questData.name == "LustElimination(Clone)"
                && InventoryManager.Instance.inventoryData.items[0].itemData.name == "PlayerBasic")
            {
                door.SetActive(true);
            }
        }
    }
}
