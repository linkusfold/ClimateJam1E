using System;
using DefaultNamespace;
using UnityEditor.Overlays;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public float countdown = 5f;
    public int maxEnemiesSafe = 4;
    public WaveData[] waves;

    public int currentWaveIndex = 0;

    [NonSerialized] public bool readyToCountDown = true;
    public bool spawnNextWave = false;
    
    public void UpdateLevel()
    {
        if (WaveSpawner.instance.EnemiesSafe >= maxEnemiesSafe)
        {
            Application.Quit();
        }

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("You survived every wave!");
            return;
        }
        
        if (readyToCountDown)
        {
            WaveSpawner.instance.levelCountdown -= Time.deltaTime;
            Debug.Log("Counting Down!");
        }

        if (WaveSpawner.instance.levelCountdown <= 0)
        {
            readyToCountDown = false;
            WaveSpawner.instance.levelCountdown = waves[currentWaveIndex].timeToNextWave;
            spawnNextWave = true;
            Debug.Log("Spawning next wave");
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
            Debug.Log("Next Wave");
        }
    }


    public void OnEnemyRemoved()
    {
        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].enemiesLeft--;
            Debug.Log("Losing enemies");
        }
    }
}

