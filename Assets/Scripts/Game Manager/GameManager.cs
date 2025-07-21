using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int playerHealth;
        [Header("Script References")]
        private WaveSpawner waveSpawner;
        public PauseMenu pauseMenu;
        public LevelData levelData;
    
        #region Unity Event Functions

        private bool isGameScene;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        
            DontDestroyOnLoad(this);

            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                isGameScene = true;
                return;
            }
            isGameScene = false;
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
