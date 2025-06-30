using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerHealth;
    public WaveSpawner waveSpawner;
    

    public LevelData levelData;
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }



        DontDestroyOnLoad(this);
    }

    void Start()
    {
        waveSpawner.StartWaveSpawning();
    }

    public void doThing()
    {
        Debug.Log("Doing something!");
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in waveSpawner.enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

}
