using System;
using UnityEngine;
using System.Collections;
using DefaultNamespace;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [SerializeField] private LevelData levelData;
    [NonSerialized] public float levelCountdown;
    private WaveData[] waveDatas;

    [SerializeField] public Transform spawnPoint;

    public int EnemiesSafe = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelCountdown = levelData.countdown;
        levelData.readyToCountDown = true;
        waveDatas = levelData.waves;

        foreach (var wave in waveDatas)
        {
            wave.ResetEnemiesLeft();
        }
    }

    private bool waveSpawning = false;

    private void Update()
    {
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

    private IEnumerator SpawnWave()
    {
        levelData.spawnNextWave = false;

        if (levelData.currentWaveIndex >= levelData.waves.Length)
            yield break;

        WaveData wave = levelData.waves[levelData.currentWaveIndex];
        wave.ResetEnemiesLeft();

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            Enemy enemy = Instantiate(wave.enemies[i], spawnPoint.position, Quaternion.identity, spawnPoint);
            enemy.path = Path.instance;
            enemy.levelData = levelData;
            enemy.currentNodeId = 1;

            yield return new WaitForSeconds(wave.timeToNextEnemy);
        }

        levelData.waveSpawned = true;
        Debug.Log("Wave " + levelData.currentWaveIndex + " spawned.");


    }
}