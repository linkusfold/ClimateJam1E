using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerHealth;
    [Header("Script References")]
    public CharacterHouse currentDialogue;
    public WaveSpawner waveSpawner;
    public PauseMenu pauseMenu;

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
        /*
        waveSpawner.StartWaveSpawning();
        */
    }

    // Testing function
    public void doThing()
    {
        Debug.Log("Doing something!");
    }



    // Game Loop Control
    public void PauseAllEnemies()
    {
        /*
        foreach (Enemy enemy in waveSpawner.enemies)
        {
            enemy.StopPathing();
        }
        */

    }
    public void ResumeAllEnemies()
    {
        /*
        foreach (Enemy enemy in waveSpawner.enemies)
        {
            enemy.StartPathing();
        }
        */
    }
    public void DestroyAllEnemies()
    {
        /*
        foreach (Enemy enemy in waveSpawner.enemies)
        {
            Destroy(enemy.gameObject);
        }
        */
    }

}
