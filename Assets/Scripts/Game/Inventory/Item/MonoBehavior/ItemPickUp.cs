using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;

    void Start()
    {
        if (itemData.isPicked)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
            InventoryManager.Instance.inventoryUI.RefreshUI();

            itemData.isPicked = true;

            if (QuestManager.Instance.IsInitialized)
                QuestManager.Instance.UpdateQuestProgress(itemData.itemName, itemData.itemAmount);

            Destroy(gameObject);
        }
    }
}