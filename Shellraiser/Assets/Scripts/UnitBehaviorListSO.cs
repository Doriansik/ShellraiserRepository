using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitBehaviorListSO", menuName = "Scriptable Objects/UnitBehaviorListSO")]
public class UnitBehaviorListSO : ScriptableObject
{
    public List<UnitBehaviorSO> unitType;
}
