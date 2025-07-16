using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Random = System.Random;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    
    public List<Path> paths = new List<Path>();

    public bool initialized = false;
    [NonSerialized] public LevelData levelData;
    [NonSerialized] public float levelCountdown;
    private WaveData[] waveDatas;

    [SerializeField] public Transform spawnPoint;

    public GameObject winScreen;

    public int EnemiesSafe = 0;
    public int EnemiesAlive = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;
        levelCountdown = levelData.countdown;
        levelData.currentWaveIndex = 0;
        levelData.readyToCountDown = true;
        levelData.spawnNextWave = false;
        waveDatas = levelData.waves;

        foreach (var wave in waveDatas)
        {
            wave.ResetEnemiesLeft();
        }
        initialized = true;
    }

    private bool waveSpawning = false;

    private void Update()
    {
        if (!initialized) return;
        
        levelData.UpdateLevel();

        if (levelData.spawnNextWave && !waveSpawning)
        {
            waveSpawning = true;
            StartCoroutine(SpawnWave());
        }

        // Reset flag once coroutine is done
        if (!levelData.spawnNextWave && waveSpawning)
        {
            waveSpawning = false;
        }
    }
    
    public void Restart()
    {
        levelData.currentWaveIndex = 0;
        EnemiesSafe = 0;

        levelCountdown = levelData.countdown;

        for (int i = 0; i < levelData.waves.Length; i++)
        {
            levelData.waves[i].enemiesLeft = levelData.waves[i].enemies.Length;
        }
    }

    private IEnumerator SpawnWave()
    {
        levelData.spawnNextWave = false;

        if (levelData.currentWaveIndex >= levelData.waves.Length)
            yield break;

        WaveData wave = levelData.waves[levelData.currentWaveIndex];
        wave.ResetEnemiesLeft();

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            Enemy enemyPrefab = wave.enemies[i];

            if (enemyPrefab is Enemy pathingEnemyPrefab)
            {
                Enemy pathingEnemy = Instantiate(pathingEnemyPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                pathingEnemy.path = paths[UnityEngine.Random.Range(0, paths.Count)];
                pathingEnemy.levelData = levelData;
                pathingEnemy.currentNodeId = 1;
            }
            else
            {
                Enemy enemy = Instantiate(enemyPrefab, spawnPoint.transform);
                enemy.transform.SetParent(spawnPoint.transform);
            }

            EnemiesAlive++;
            yield return new WaitForSeconds(wave.timeToNextEnemy);
        }

        levelData.waveSpawned = true;
        Debug.Log("Wave " + levelData.currentWaveIndex + " spawned.");
    }
}