using System;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    public Enemy[] enemies;
    [NonSerialized] public Path path;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;

    public void ResetEnemiesLeft()
    {
        enemiesLeft = enemies.Length;
    }
}
