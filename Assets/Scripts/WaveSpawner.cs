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

    private void Update()
    {
        levelData.UpdateLevel();
        if (levelData.spawnNextWave)
        {
            StartCoroutine(SpawnWave());
            Debug.Log("Wave Spawned");
        }
    }

    private IEnumerator SpawnWave()
    {
        levelData.spawnNextWave = false;
        if (levelData.currentWaveIndex < levelData.waves.Length)
        {
            var wave = levelData.waves[levelData.currentWaveIndex];

            for (int i = 0; i < wave.enemies.Length; i++)
            {
                wave.enemies[i].path = Path.instance;

                Enemy enemy = Instantiate(wave.enemies[i], spawnPoint.transform);
                enemy.levelData = levelData;
                enemy.currentNodeId = 1;
                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(wave.timeToNextEnemy);
            }
        }
    }
}
