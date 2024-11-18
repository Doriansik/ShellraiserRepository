using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Spawner Variables")]
    [SerializeField] private float timeBetweenSpawn;
    private float spawnRate;

    [Header("Other")]
    [SerializeField] private EnemyBehaviorSO enemyOrdinary;
    [SerializeField] private GameObject homeEnemyUnitSpawnpoint;
    [SerializeField] private GameObject homeUnitSpawnpoint;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private EnemyBehaviorListSO enemyListType;

    private void Start()
    {
        spawnRate = timeBetweenSpawn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(unitPrefab, homeUnitSpawnpoint.transform.position, Quaternion.identity);
        }

        //Spawn();
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

}
