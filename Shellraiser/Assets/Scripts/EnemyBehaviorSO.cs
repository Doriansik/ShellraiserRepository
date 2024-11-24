using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehaviorSO", menuName = "Scriptable Objects/EnemyBehaviorSO")]
public class EnemyBehaviorSO : ScriptableObject
{
    [Header("Statystyki jednostki")]
    public float moveSpeed; // Predkosc poruszania sie
    public int damage; // Ilosc zadawanych obrazen
    public float attackCooldown; // Czas miedzy atakami
    public int health; // Punkty zycia jednostki
    public float attackRange;
    public Vector3 spawnRotation; // Rotacja przeciwnika po spawnie
    public GameObject prefab; // Prefab wroga
    public bool isRanged;
}
