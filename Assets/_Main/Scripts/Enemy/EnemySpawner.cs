using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> enemyPrefab;
    public Transform spawnArea;
    public int maxEnemies = 5;
    public float spawnInterval = 3f;

    private int currentEnemyCount;
    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnArea != null)
        {
            Bounds bounds = spawnArea.GetComponent<Collider2D>().bounds;
            Vector2 spawnPosition = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            int randomRange = Random.Range(0, enemyPrefab.Count);
            GameObject enemy = Instantiate(enemyPrefab[randomRange], spawnPosition, Quaternion.identity);
            enemy.GetComponent<Enemy>().spawnArea = spawnArea;

            currentEnemyCount++;
        }
    }
}
