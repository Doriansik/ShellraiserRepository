using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "Scriptable Objects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public float attackRange;
    public float attackCooldown;
    public float damage;
    public GameObject projectile;
    public Transform projectileSpawnpoint;
}
