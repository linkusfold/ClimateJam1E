using System;
using DefaultNamespace;
using UnityEngine;
using System.Collections;
using Game_Manager;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public float countdown = 5f;
    public WaveData[] waves;
    public Boss boss;

    public int currentWaveIndex = 0;

    [NonSerialized] public bool readyToCountDown = false;
    [NonSerialized] public bool spawnNextWave = false;
    [NonSerialized] public bool waveSpawned = false;

    public void UpdateLevel()
    {
        if (currentWaveIndex >= waves.Length && WaveSpawner.instance.EnemiesAlive == 0)
        {
            Debug.Log("LevelData: Update(): All waves completed.");
            return;
        }

        // Start countdown to next wave
        if (readyToCountDown)
        {
            WaveSpawner.instance.levelCountdown -= Time.deltaTime;
            if (WaveSpawner.instance.levelCountdown <= 0f)
            {
                readyToCountDown = false;
                spawnNextWave = true;
                Debug.Log("Countdown finished, spawning next wave.");
            }
        }

        // If wave is spawned and all enemies are dead, advance to next wave index
        if (waveSpawned) //&& waves[currentWaveIndex].enemiesLeft <= 0)
        {
            currentWaveIndex++;
            Debug.Log($"Wave completed, advancing index to {currentWaveIndex}.");
            waveSpawned = false;

            if (currentWaveIndex < waves.Length)
            {
                readyToCountDown = true;
                WaveSpawner.instance.levelCountdown = waves[currentWaveIndex].timeToNextWave;
            }
            
        }
    }

    public void OnEnemyRemoved()
    {
        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].enemiesLeft--;
            //Debug.Log("Enemy removed. Remaining:  + waves[currentWaveIndex].enemiesLeft);
        }
        if (currentWaveIndex >= waves.Length && WaveSpawner.instance.EnemiesAlive <= 0)
        {
            Debug.Log("All waves completed.");
            GameManager.instance.Win();
        }
    }
}
