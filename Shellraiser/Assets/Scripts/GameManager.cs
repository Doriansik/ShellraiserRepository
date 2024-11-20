using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Spawner Variables")]
    [SerializeField] private float timeBetweenSpawn;
    private float spawnRate;

    [Header("Shop Variables")]
    [SerializeField] private int coinAmount;
    [SerializeField] private float timeToEarnCoin;
    [SerializeField] private TextMeshProUGUI coinText;
    private float coinTimer;


    [Header("Other")]
    [SerializeField] private GameObject homeEnemyUnitSpawnpoint;
    [SerializeField] private GameObject homeUnitSpawnpoint;
    [SerializeField] private UnitBehaviorListSO unitListType;
    [SerializeField] private EnemyBehaviorListSO enemyListType;

    private void Start()
    {
        spawnRate = timeBetweenSpawn;
    }

    private void Update()
    {
        EarningCoin();
        Spawn();
    }


    private void EarningCoin()
    {
        // Odliczanie czasu
        coinTimer += Time.deltaTime;

        // Jeœli min¹³ czas do zdobycia monety
        if (coinTimer >= timeToEarnCoin)
        {
            coinAmount++; // Dodanie monety
            coinText.text = coinAmount.ToString();
            coinTimer = 0f; // Resetowanie licznika
            Debug.Log("Zdoby³eœ monetê! Aktualna liczba monet: " + coinAmount);
        }
    }

    private void Spawn()
    {
        // Odliczanie do kolejnego spawnu
        spawnRate -= Time.deltaTime;
        if (spawnRate <= 0f)
        {
            // Losowanie przeciwnika z listy
            EnemyBehaviorSO randomEnemyType = enemyListType.enemyType[Random.Range(0, enemyListType.enemyType.Count)];

            // Spawnowanie przeciwnika
            Instantiate(randomEnemyType.prefab, homeEnemyUnitSpawnpoint.transform.position, Quaternion.identity);

            // Resetowanie czasu do nastêpnego spawnu
            spawnRate = timeBetweenSpawn;
        }
    }


    public void OnSpawnOrdinary()
    {
        UnitBehaviorSO unitOrdinary = unitListType.unitType[0];
        if (coinAmount >= unitOrdinary.cost)
        {
            Instantiate(unitOrdinary.prefab, homeUnitSpawnpoint.transform.position, Quaternion.identity);
            coinAmount -= unitOrdinary.cost;
        }
    }

    public void OnSpawnRare()
    {
        UnitBehaviorSO unitRare = unitListType.unitType[1];
        if (coinAmount >= unitRare.cost)
        {
            Instantiate(unitRare.prefab, homeUnitSpawnpoint.transform.position, Quaternion.identity);
            coinAmount -= unitRare.cost;
        }
    }


    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
