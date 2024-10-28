using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { BAG, WEAPON, ARMOR, ACTION }
public class SlotHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemUI itemUI;
    public SlotType slotType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0)
        {
            UseItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI.GetItem())
        {
            InventoryManager.Instance.itemTooltip.SetUpTooltip(itemUI.GetItem());
            InventoryManager.Instance.itemTooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.itemTooltip.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        if (itemUI.GetItem() != null && slotType == SlotType.BAG)
        {
            if (itemUI.GetItem().itemType == ItemType.Usable && itemUI.Bag.items[itemUI.Index].amount > 0)
            {
                PlayerNumController.Instance.LightChange(itemUI.GetItem().usableItemData.RestoreLightPoint);
                itemUI.Bag.items[itemUI.Index].amount--; // decrease the amount by 1
            }
            else if (itemUI.GetItem().itemType == ItemType.Weapon && itemUI.Bag.items[itemUI.Index].amount > 0)
            {
                if (InventoryManager.Instance.actionData.items[0] == new InventoryItem())
                {
                    InventoryManager.Instance.actionData.items[0] = new InventoryItem(itemUI.GetItem(), 1);
                    itemUI.Bag.items[itemUI.Index] = new InventoryItem();
                }
                else
                {
                    var temItem = InventoryManager.Instance.actionData.items[0];
                    InventoryManager.Instance.actionData.items[0] = new InventoryItem(itemUI.GetItem(), 1);
                    itemUI.Bag.items[itemUI.Index] = temItem;
                }
                InventoryManager.Instance.actionUI.slots[0].UpdateItem();
            }
        }
        UpdateItem();

        // QuestManager.Instance.UpdateQuestProgress(itemUI.GetItem().itemName, -1);
    }

    public void UpdateItem()
    {
        switch (slotType)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            /*case SlotType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                    GameManager.Instance.playerStatus.SwitchWeapon(itemUI.Bag.items[itemUI.Index].itemData);
                else
                    GameManager.Instance.playerStatus.UnEquipWeapon();
                break;
            case SlotType.ARMOR:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                    GameManager.Instance.playerStatus.SwitchArmor(itemUI.Bag.items[itemUI.Index].itemData);
                else
                    GameManager.Instance.playerStatus.UnEquipArmor();
                break;*/
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instance.actionData;
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                    PlayerController.Instance.EquipWeapon();
                else
                    PlayerController.Instance.UnEquipWeapon();
                break;
        }

        var item = itemUI.Bag.items[itemUI.Index];
        if (slotType == SlotType.BAG)
            itemUI.SetUpItemUI(item.itemData, item.amount);
        else if (slotType == SlotType.ACTION)
            itemUI.SetUpItemUI(item.itemData);
    }

    void OnDisable()
    {
        InventoryManager.Instance.itemTooltip.gameObject.SetActive(false);
    }
}