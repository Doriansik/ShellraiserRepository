using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/EnemyBehaviorListSO")]
public class EnemyBehaviorListSO : ScriptableObject
{
    public List<EnemyBehaviorSO> enemyType;
}
