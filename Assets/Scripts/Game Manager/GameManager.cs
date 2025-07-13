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

        public enum PlacementMode
        {
            None = 0,
            Placement = 1,
            Destruction = 2
        }
        public PlacementMode placementMode = PlacementMode.None;
        public Tower selectedTower;
    
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
            waveSpawner = WaveSpawner.instance;
        
        
            waveSpawner.Initialize(levelData);
            GenerateGrid();
        }

        public void Update()
        {
            if (placementMode == PlacementMode.None) return;
        
            PlaceTower();
        }
        
        void OnDrawGizmos()
        {
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


        public void SelectTower(Tower tower)
        {
            selectedTower = tower;
            placementMode = PlacementMode.Placement;
        }

        public void RemoveTower(Tower tower)
        {
            
        }
        

        public void PlaceTower()
        {
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
                    Instantiate(selectedTower, snapPos, Quaternion.identity);
                    cell.Occupy();
                    placementMode = PlacementMode.None;
                }
            }
        }
    
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
