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

        Spawn();
    }




    private void Spawn()
    {
        spawnRate -= Time.deltaTime;
        if (spawnRate <= 0f)
        {
            Instantiate(enemyOrdinary.prefab, homeEnemyUnitSpawnpoint.transform.position, Quaternion.identity);
            spawnRate = timeBetweenSpawn;
        }
    }
}
