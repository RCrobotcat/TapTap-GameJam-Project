using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;

    void Start()
    {
        if (itemData.isPicked || InventoryManager.Instance.ContainsRage())
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (itemData.isPicked || InventoryManager.Instance.ContainsRage())
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

            SaveManager.Instance.SavePlayerData();
            SaveManager.Instance.SavePlayerPosition();
            QuestUI.Instance.SaveCompletedText.SetActive(true);

            // Set the QuestCompletedText to inactive after 2 seconds
            StartCoroutine(SaveManager.Instance.DeactivateSaveCompletedText());

            Destroy(gameObject);
        }
    }
}