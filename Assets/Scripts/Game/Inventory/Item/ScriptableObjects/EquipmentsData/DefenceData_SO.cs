using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defend Data", menuName = "Defend/Data")]
public class DefenceData_SO : ScriptableObject
{
    [Header("Armor or Shield Defence Added")]
    public int AddDefence;
}
