using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game_Manager
{
    [Serializable]
    public enum Homes
    {
        archie,
        leah,
        georgie,
        finn,
        diogini,
        playerHome

    }

    public enum StoryChoices
    {
        givePocketKnife,
        archiePath,
    }
    public class GameManager : MonoBehaviour
    {
        public int CommunityPortion = 0;
        public int[] storyEvents = new int[Enum.GetNames(typeof(StoryChoices)).Count()];
        public static GameManager instance;
        // public House[] houses = new House[5];
        public bool inConversation;
        public int playerHealth;
        [Header("Script References")]
        private WaveSpawner waveSpawner;
        public PauseMenu pauseMenu;

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

        }
        
        //OnEnable comes first
        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Debug.Log("OnEnable");
        }

        //OnDisable last.
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            Debug.Log("OnDisable");
        }

        //SceneLoad comes second.
        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("GameManager: Start() called.");
            if(WaveSpawner.instance) isGameScene = true;

            if(!isGameScene) return;
            
            waveSpawner = WaveSpawner.instance;
            
            isGameOver = false;
            
            waveSpawner.Initialize();
            GenerateGrid();
        }
        

        void Update() 
        {
            if (!isGameScene) return;

            switch (placementMode)
            {
                case PlacementMode.None:
                    return;
                case PlacementMode.Placement:
                    PlaceTower();
                    break;
                case PlacementMode.Destruction:
                    break;
            }
        }

        void OnDrawGizmos()
        {
            if (SceneManager.GetActiveScene().name != "GameScene") return;
            Gizmos.color = Color.green;

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Vector3 cellCenter = GridToWorld(new Vector2Int(x, y)) + new Vector3(cellSize / 2f, cellSize / 2f, 0f);
                    Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, cellSize, 0f));
                }
            }
        }
        #endregion

        #region Tower Placement Functions

        private enum PlacementMode { None, Placement, Destruction }
        private PlacementMode placementMode = PlacementMode.None;
        private Tower selectedTower;
        [NonSerialized] public TowerButton btn;

        public void SelectTower(Tower tower)
        {
            if (!isGameScene) return;

            selectedTower = tower;
            placementMode = PlacementMode.Placement;
        }

        public void RemoveTower(Tower tower)
        {
            if (!isGameScene) return;

            Vector2Int gridPos = WorldToGrid(tower.transform.position);
            GridCell cell = GetCell(gridPos);

            if (cell != null)
            {
                cell.Vacate();
            }

            Destroy(tower.gameObject);

        }


        public void PlaceTower()
        {
            if (!isGameScene) return;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0;

                Vector2Int gridPos = WorldToGrid(worldPos);
                GridCell cell = GetCell(gridPos);
                Vector3 snapPos = GridToWorld(gridPos) + new Vector3(cellSize / 2f, cellSize / 2f, 0f);


                Debug.Log($"Mouse World Pos: {worldPos}, Grid: {gridPos}, Snapped: {snapPos}");

                if (cell != null && !cell.isOccupied)
                {
                    btn.tower = Instantiate(selectedTower, snapPos, Quaternion.identity);
                    cell.Occupy();
                    placementMode = PlacementMode.None;
                }
            }
        }
        #endregion

        #region Grid Logic
        [Header("Grid")]
        public GameObject cellPrefab;
        public int rows = 5;
        public int columns = 9;
        public float cellSize = 1f;

        public Vector2 gridOrigin = Vector2.zero;

        private GridCell[,] grid;
        void GenerateGrid()
        {
            if (cellPrefab == null) return;
            if (!isGameScene) return;

            grid = new GridCell[columns, rows];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Vector3 spawnPosition = new Vector3((x * cellSize), (y * cellSize), 0f) + (Vector3)gridOrigin;
                    GameObject cell = Instantiate(cellPrefab, spawnPosition, Quaternion.identity, transform);
                    GridCell gridCell = cell.GetComponent<GridCell>();
                    gridCell.gridPosition = new Vector2Int(x, y);
                    grid[x, y] = gridCell;
                }
            }
        }

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - (Vector3)gridOrigin;

            float gridX = localPos.x / cellSize;
            float gridY = localPos.y / cellSize;

            int x = Mathf.Clamp(Mathf.FloorToInt(gridX), 0, columns - 1);
            int y = Mathf.Clamp(Mathf.FloorToInt(gridY), 0, rows - 1);

            return new Vector2Int(x, y);
        }

        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0f) + (Vector3)gridOrigin;
        }

        public GridCell GetCell(Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= columns || pos.y >= rows) return null;
            return grid[pos.x, pos.y];
        }
        #endregion

        #region End Round Logic

        [Header("End Round Variables")]
        public GameObject winScreen;
        public AudioClip winSound;
        public GameObject loseScreen;
        public AudioClip loseSound;
        private bool isGameOver = false;
        [NonSerialized] public List<House> houses = new List<House>();

        public bool CheckHousesAlive()
        {
            House aliveHouse = houses.Find(r => !r.IsDestroyed);
            Debug.Log($"Houses: {houses.Count}, Alive: {aliveHouse}");
            return aliveHouse != null;
        }

        public void Win()
        {
            Debug.Log("GameManager: Win() called.");
            if (isGameOver) return;
            if (winScreen.activeInHierarchy) return;
            if (!winScreen)
            {
                Debug.LogError("GameManager: Win(): No winScreen specified.");
                return;
            }
            if (winSound) AudioSource.PlayClipAtPoint(winSound, transform.position);
            Instantiate(winScreen, FindFirstObjectByType<Canvas>().gameObject.transform);
        }

        public void Lose()
        {
            Debug.Log("GameManager: Lose() called.");
            if (isGameOver) return;
            if (loseScreen.activeInHierarchy) return;
            if (!loseScreen)
            {
                Debug.LogError("GameManager: Lose(): No loseScreen specified.");
                return;
            }
            if (loseSound) AudioSource.PlayClipAtPoint(loseSound, transform.position);
            Instantiate(loseScreen, FindFirstObjectByType<Canvas>().gameObject.transform);
        }

        public static void NextButton()
        {
            Debug.Log("GameManager: NextButton() called.");
            if(SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1) SceneManager.LoadScene(0);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void QuitButton()
        {
            Debug.Log("GameManager: QuitButton() called.");
            Application.Quit();
        }

        public static void RetryButton()
        {
            Debug.Log("GameManager: RetryButton() called.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion

        #region Passing Data To Dialogue Scene

        public List<House> destroyedHouses;
        private List<House> StoreDestroyedHouses()
        {
            if (!isGameScene) return null;
            return houses.GroupBy(r => r.isDestroyed).Select(r => r.First()).ToList();
        }

        public bool isHouseDestroyed(Homes home)
        {

            // testing
            if (home == Homes.archie)
            {
                Debug.Log("Archie!");
            }


            Debug.LogWarning("Implement");
            // TODO: Implement
            return false;
        }
        #endregion

        #region Passing Dialogue Data To Next Level

        public void UpdateHouses()
        {
            for (int houseIndex = 0; houseIndex < Enum.GetNames(typeof(Homes)).Count(); houseIndex++)
            {
                // update towers if valid
            }
        }

        #endregion

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
}
