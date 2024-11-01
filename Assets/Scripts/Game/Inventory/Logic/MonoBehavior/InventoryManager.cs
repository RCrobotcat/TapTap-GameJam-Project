using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : Singleton<InventoryManager>
{
    // use to store the original holder and parent of the item being dragged
    // convienient to return the item to its original slot
    public class DragData
    {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }

    [Header("Inventory Data")]
    public InventoryData_SO inventoryTemplate;
    [HideInInspector] public InventoryData_SO inventoryData;

    public InventoryData_SO actionTemplate;
    [HideInInspector] public InventoryData_SO actionData;

    // public InventoryData_SO equipmentTemplate;
    // public InventoryData_SO equipmentData;

    [Header("Containers")]
    public ContainerUI inventoryUI;
    public ContainerUI actionUI;
    // public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;

    [Header("UI Panels")]
    public GameObject BagPanel;
    public GameObject EnemyHealthPanel;
    // public GameObject EquipmentPanel;

    /*[Header("Status Text")]
    public Text healthText;
    public Text attaclText;
    public Text DefenceText;*/

    [Header("Tooltip")]
    public ItemTooptip itemTooltip;

    bool isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        if (inventoryTemplate != null)
            inventoryData = Instantiate(inventoryTemplate);
        if (actionTemplate != null)
            actionData = Instantiate(actionTemplate);
        /*if (equipmentTemplate != null)
            equipmentData = Instantiate(equipmentTemplate);*/
    }

    void Start()
    {
        LoadData();
        inventoryUI.RefreshUI();
        actionUI.RefreshUI();
        // equipmentUI.RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && SceneManager.GetActiveScene().name != "MenuScene")
        {
            isOpen = !isOpen;
            BagPanel.SetActive(isOpen);
            // EquipmentPanel.SetActive(isOpen);
        }

        /*UpdateStatusText(GameManager.Instance.playerStatus.currentHealth,
            GameManager.Instance.playerStatus.attackData.minDamage,
            GameManager.Instance.playerStatus.attackData.maxDamage,
            GameManager.Instance.playerStatus.currentDefence);*/
    }

    #region Save Data
    public void SaveData()
    {
        SaveManager.Instance.Save(inventoryData, inventoryData.name);
        SaveManager.Instance.Save(actionData, actionData.name); ;
    }

    public void LoadData()
    {
        SaveManager.Instance.Load(inventoryData, inventoryData.name);
        SaveManager.Instance.Load(actionData, actionData.name);
    }
    #endregion

    public bool ContainsRage()
    {
        foreach (var item in inventoryData.items)
        {
            if (item.itemData != null && item.itemData.name == "Rage")
                return true;
        }
        if (actionData.items[0].itemData != null && actionData.items[0].itemData.name == "Rage")
            return true;
        return false;
    }

    #region Judge the item being dragged is inside the range of the target slot
    public bool CheckInInventoryUI(Vector3 position)
    {
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            // same as => (RectTransform) inventoryUI.slots[i].transform, typecasting
            RectTransform t = inventoryUI.slots[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
                return true;
        }
        return false;
    }

    public bool CheckInActionUI(Vector3 position)
    {
        for (int i = 0; i < actionUI.slots.Length; i++)
        {
            // same as => (RectTransform) inventoryUI.slots[i].transform, typecasting
            RectTransform t = actionUI.slots[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
                return true;
        }
        return false;
    }

    /*public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentUI.slots.Length; i++)
        {
            // same as => (RectTransform) inventoryUI.slots[i].transform, typecasting
            RectTransform t = equipmentUI.slots[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
                return true;
        }
        return false;
    }*/
    #endregion

    #region Check if the quest item already exists in the inventory, if so, update the quest progress
    public void CheckQuestItemInBag(string questItemName)
    {
        foreach (var item in inventoryData.items)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                    QuestManager.Instance.UpdateQuestProgress(questItemName, item.amount);
            }
        }

        foreach (var item in actionData.items)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                    QuestManager.Instance.UpdateQuestProgress(questItemName, item.amount);
            }
        }
    }
    #endregion

    #region Check if the quest item is in the inventory or action slot
    public InventoryItem QuestItemInBag(ItemData_SO questItem)
    {
        return inventoryData.items.Find(i => i.itemData == questItem);
    }

    public InventoryItem QuestItemInActionBar(ItemData_SO questItem)
    {
        return actionData.items.Find(i => i.itemData == questItem);
    }
}
#endregion
