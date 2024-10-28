using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Usable Item Data")]
public class UsableItemData_SO : ScriptableObject
{
    [Header("Light Potion")]
    public int RestoreLightPoint;
}
