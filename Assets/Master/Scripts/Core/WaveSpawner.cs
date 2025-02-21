using Assets.Master.Scripts.Enemy.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObjectPooling;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPointData
    {
        public Transform spawnPoint;
        public EnemyBase enemyPrefab;
        public int enemyCount;
        public float spawnRate;
        public float spawnRadius = 2f;
    }

    [System.Serializable]
    public class Wave
    {
        public SpawnPointData[] spawnPoints;
        public float timeBetweenWaves = 5f;
    }

    [Header("Waves Configuration")]
    public Wave[] waves;
    public int poolSizePerEnemy = 10;
    public Transform target;

    private Dictionary<EnemyBase, ObjectPool<EnemyBase>> enemyPools = new Dictionary<EnemyBase, ObjectPool<EnemyBase>>();
    private int enemiesAlive = 0;

    private int totalEnemiesSpawned = 0;

    void Start()
    {
        InitializePools();
        StartCoroutine(WaveRoutine());
    }

    private void InitializePools()
    {
        foreach (var wave in waves)
        {
            foreach (var spawnData in wave.spawnPoints)
            {
                if (!enemyPools.ContainsKey(spawnData.enemyPrefab))
                {
                    enemyPools[spawnData.enemyPrefab] = new ObjectPool<EnemyBase>(spawnData.enemyPrefab, poolSizePerEnemy, transform);
                }
            }
        }
    }

    private IEnumerator WaveRoutine()
    {
        foreach (var wave in waves)
        {
            totalEnemiesSpawned = 0;
            enemiesAlive = 0;

            foreach (var spawnData in wave.spawnPoints)
            {
                StartCoroutine(SpawnEnemiesFromPoint(spawnData));
            }

            yield return new WaitUntil(() => enemiesAlive == 0 && totalEnemiesSpawned >= GetTotalEnemiesInWave(wave));

            Debug.Log("Wave Completed!");
            yield return new WaitForSeconds(wave.timeBetweenWaves);
        }
    }

    private int GetTotalEnemiesInWave(Wave wave)
    {
        int total = 0;
        foreach (var spawnData in wave.spawnPoints)
        {
            total += spawnData.enemyCount;
        }
        return total;
    }

    private IEnumerator SpawnEnemiesFromPoint(SpawnPointData spawnData)
    {
        for (int i = 0; i < spawnData.enemyCount; i++)
        {
            SpawnEnemy(spawnData);
            yield return new WaitForSeconds(1f / spawnData.spawnRate);
        }
    }

    private void SpawnEnemy(SpawnPointData spawnData)
    {
        if (!enemyPools.ContainsKey(spawnData.enemyPrefab))
            return;

        EnemyBase enemy = enemyPools[spawnData.enemyPrefab].Get();
        Vector3 randomOffset = new Vector3(Random.Range(-spawnData.spawnRadius, spawnData.spawnRadius), 0, Random.Range(-spawnData.spawnRadius, spawnData.spawnRadius));
        enemy.transform.position = spawnData.spawnPoint.position + randomOffset;
        enemy.Init(this);

        enemiesAlive++;
        totalEnemiesSpawned++;
    }

    public void ReturnEnemyToPool(EnemyBase enemy)
    {
        foreach (var pool in enemyPools)
        {
            if (pool.Key.GetType() == enemy.GetType()) // Check by enemy type
            {
                pool.Value.ReturnToPool(enemy);
                enemiesAlive--;

                if (enemiesAlive < 0)
                    enemiesAlive = 0;

                return;
            }
        }

        Debug.LogWarning("Enemy type not found in pool: " + enemy.GetType().Name);
    }

    //public void ReturnEnemyToPool(EnemyBase enemy)
    //{
    //    foreach (var pool in enemyPools)
    //    {
    //        if (pool.Key.GetType() == enemy.GetType()) // Check by enemy type
    //        {
    //            pool.Value.ReturnToPool(enemy);
    //            enemiesAlive--;
    //            return;
    //        }
    //    }

    //    Debug.LogWarning("Enemy type not found in pool: " + enemy.GetType().Name);
    //}
}
