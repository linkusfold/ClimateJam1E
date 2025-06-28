using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    int health = 100;
    [SerializeField]
    WaveSpawner waveSpawner;


    public LevelData levelData;
    void Awake()
    {
        Debug.Log("Awaken!");
        if (instance == null)
        {
            Debug.Log("Null");
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("not this");
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
        Debug.Log("I'm doing it!");
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in waveSpawner.enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

}
