using UnityEngine;

[CreateAssetMenu(fileName = "UnitBehaviorSO", menuName = "Scriptable Objects/UnitBehaviorSO")]
public class UnitBehaviorSO : ScriptableObject
{
    [Header("Statystyki jednostki")]
    public float moveSpeed; // Predkosc poruszania sie
    public int damage; // Ilosc zadawanych obrazen
    public float attackCooldown; // Czas miedzy atakami
    public int health; // Punkty zycia jednostki
    public int cost; // Monety potrzebne do kupienia jednostki
    public Vector3 spawnRotation; // Rotacja przeciwnika po spawnie
    public GameObject prefab; // Prefab jednostki 
    public bool isRanged;
}
