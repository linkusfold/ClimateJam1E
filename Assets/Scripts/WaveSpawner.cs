using UnityEngine;
using System.Collections;
using DefaultNamespace;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private WaveData[] waveDatas;

    [SerializeField] private Transform spawnPoint;

    private float countdown;
    public int currentWaveIndex = 0;
    public int EnemiesSafe = 0;

    private bool readyToCountDown = true;

    private void Start()
    {
        countdown = levelData.countdown;
        waveDatas = levelData.waves;

        foreach (var wave in waveDatas)
        {
            wave.ResetEnemiesLeft();
        }
    }

    private void Update()
    {
        if (EnemiesSafe >= levelData.maxEnemiesSafe)
        {
            Application.Quit();
        }

        if (currentWaveIndex >= waveDatas.Length)
        {
            Debug.Log("You survived every wave!");
            return;
        }

        if (readyToCountDown)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            readyToCountDown = false;
            countdown = waveDatas[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waveDatas[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waveDatas.Length)
        {
            var wave = waveDatas[currentWaveIndex];

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

    // Call this from Enemy when it's destroyed or completes its path
    public void OnEnemyRemoved()
    {
        if (currentWaveIndex < waveDatas.Length)
        {
            waveDatas[currentWaveIndex].enemiesLeft--;
        }
    }
}
