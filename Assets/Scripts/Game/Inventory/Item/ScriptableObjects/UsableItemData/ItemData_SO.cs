using UnityEngine;

public enum ItemType { Usable, Weapon, Armor, DropItem }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;

    public bool isPicked;

    [TextArea]
    public string description = "";

    public bool stackable;

    [Header("Weapon")]
    public GameObject WeaponPrefab;
    public AttackData_SO WeaponData;
    public AnimatorOverrideController WeaponAnimator;

    [Header("Armor")]
    public GameObject ArmorPrefab;
    public DefenceData_SO ArmorDefenceData;
    public AnimatorOverrideController ArmorAnimator;

    [Header("Usable Item")]
    public UsableItemData_SO usableItemData;
}
