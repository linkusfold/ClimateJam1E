using DefaultNamespace;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// Deprecated
/// </summary>
public class WaveSpawnerBOSS : MonoBehaviour
{
    [SerializeField] private float countdown;

    [SerializeField] private GameObject spawnPoint;

    public WaveData[] waves;

    public int currentWaveIndex = 0;

    private bool readyToCountDown;

    public int EnemiesSafe = 0;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        currentWaveIndex = 0;
        EnemiesSafe = 0;

        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    private void Update()
    {
        //if (EnemiesSafe >= 4)
        //{
        //    Application.Quit();
        //}

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("You survived every wave!");
            return;
        }

        if (readyToCountDown == true)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            readyToCountDown = false;
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());

            Debug.Log("Starting wave " + currentWaveIndex);
        }
        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemyPrefab = waves[currentWaveIndex].enemies[i];

                if (enemyPrefab is Enemy pathingEnemyPrefab)
                {
                    pathingEnemyPrefab.path = waves[currentWaveIndex].path;

                    Enemy pathingEnemy = Instantiate(pathingEnemyPrefab, spawnPoint.transform);
                    pathingEnemy.currentNodeId = 1;
                    pathingEnemy.transform.SetParent(spawnPoint.transform);
                }
                else
                {
                    Enemy enemy = Instantiate(enemyPrefab, spawnPoint.transform);
                    enemy.transform.SetParent(spawnPoint.transform);
                }

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}
