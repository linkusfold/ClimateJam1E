using UnityEngine;
using System.Collections;
using DefaultNamespace;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private WaveData[] waveDatas;
    [SerializeField] public Transform spawnPoint;

    private void Start()
    {
        waveDatas = levelData.waves;

        foreach (var wave in waveDatas)
        {
            wave.ResetEnemiesLeft();
        }
    }

    private void Update()
    {
        levelData.updateLevel();
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
                enemy.currentNodeId = 1;
                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(wave.timeToNextEnemy);
            }
        }
    }
}
