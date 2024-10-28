using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    public SlotHolder[] slots;
    public Button SortItemsBtn;

    void Start()
    {
        if (SortItemsBtn != null)
            SortItemsBtn.onClick.AddListener(SortItems);
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].itemUI.Index = i;
            slots[i].UpdateItem();
        }
    }

    // Sort items by item type
    public void SortItems()
    {
        var bag = InventoryManager.Instance.inventoryData;
        var items = bag.items;

        var sortedItems = items
            .Where(itemSlot => itemSlot.itemData != null)
            .OrderBy(itemSlot =>
            {
                // ������Ʒ���͵����ȼ�
                if (itemSlot.itemData.itemType == ItemType.Weapon)
                    return 0;
                else if (itemSlot.itemData.itemType == ItemType.DropItem)
                    return 1;
                else
                    return 2;
            })
            .ThenByDescending(itemSlot =>
                // �����������͵���Ʒ��������С�˺�ֵ��������
                itemSlot.itemData.itemType == ItemType.Weapon && itemSlot.itemData.WeaponData != null
                    ? itemSlot.itemData.WeaponData.minDamage
                    : 0)
            .ThenBy(itemSlot =>
                // �Է��������͵���Ʒ��������������
                itemSlot.itemData.itemType != ItemType.Weapon
                    ? itemSlot.itemData.itemName
                    : "")
            .ToList();

        int nullItemCount = items.Count - sortedItems.Count;

        // ������Ʒ��ӵ��б�ĩβ
        for (int i = 0; i < nullItemCount; i++)
        {
            sortedItems.Add(new InventoryItem());
        }

        bag.items = sortedItems;
        RefreshUI();
    }
}
