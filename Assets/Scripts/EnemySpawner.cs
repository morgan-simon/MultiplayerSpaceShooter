using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    public float spawnInterval = 1f;

    private float spawnTimer = 0f;
    void Update()
    {
        if (!IsServer)
            return;

        // Increment spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset spawn timer
        }
    }

    void SpawnEnemy()
    {

        int numEnemies = Random.Range(3, 5);

        for (int i = 0; i < numEnemies; i++)
            {
                // Spawn within these coordinates
                Vector3 spawnPosition = new Vector3(Random.Range(-1f, 7f), Random.Range(-4f, 4f), 0f);

                // Spawn the enemy at the calculated spawn position
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Spawn the enemy over the network
                NetworkObject networkObject = enemy.GetComponent<NetworkObject>();
                networkObject.Spawn();
            }
    }
}
