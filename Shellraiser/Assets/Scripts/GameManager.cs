using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Spawner Variables")]
    [SerializeField] private float timeBetweenSpawn;
    private float spawnRate;

    [Header("Shop Variables")]
    [SerializeField] private float buyDelayAmount;
    [SerializeField] private int coinAmount;
    [SerializeField] private float timeToEarnCoin;
    [SerializeField] private TextMeshProUGUI coinText;
    private float coinTimer;
    private float buyDelay;


    [Header("Other")]
    [SerializeField] private GameObject homeEnemyUnitSpawnpoint;
    [SerializeField] private GameObject homeUnitSpawnpoint;
    [SerializeField] private UnitBehaviorListSO unitListType;
    [SerializeField] private EnemyBehaviorListSO enemyListType;

    private void Start()
    {
        spawnRate = timeBetweenSpawn;
        buyDelay = buyDelayAmount;

    }

    private void Update()
    {
        if (buyDelay > 0f)
        {
            buyDelay -= Time.deltaTime;
        }

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

            // Tworzenie rotacji na podstawie danych z ScriptableObject
            Quaternion spawnRotation = Quaternion.Euler(randomEnemyType.spawnRotation);

            // Spawnowanie przeciwnika
            Instantiate(randomEnemyType.prefab, homeEnemyUnitSpawnpoint.transform.position, spawnRotation);

            // Resetowanie czasu do nastêpnego spawnu
            spawnRate = timeBetweenSpawn;
        }
    }



    public void OnSpawnOrdinary()
    {
        UnitBehaviorSO unitOrdinary = unitListType.unitType[0];

        if (buyDelay <= 0f && coinAmount >= unitOrdinary.cost)
        {
            coinAmount -= unitOrdinary.cost;
            // Tworzenie jednostki
            Instantiate(unitOrdinary.prefab, homeUnitSpawnpoint.transform.position, Quaternion.Euler(unitOrdinary.spawnRotation));
            buyDelay = buyDelayAmount;
        }
    }

    public void OnSpawnRare()
    {
        UnitBehaviorSO unitRare = unitListType.unitType[1];
        if (buyDelay <= 0f && coinAmount >= unitRare.cost)
        {
            coinAmount -= unitRare.cost;
            Instantiate(unitRare.prefab, homeUnitSpawnpoint.transform.position, Quaternion.identity);
            buyDelay = buyDelayAmount;
        }
    }

    public void OnSpawnLegendary()
    {
        UnitBehaviorSO unitLegendary = unitListType.unitType[2];
        if (buyDelay <= 0f && coinAmount >= unitLegendary.cost)
        {
            coinAmount -= unitLegendary.cost;
            Instantiate(unitLegendary.prefab, homeUnitSpawnpoint.transform.position, Quaternion.Euler(unitLegendary.spawnRotation));
            buyDelay = buyDelayAmount;
        }
    }


    public void OnRestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
}
