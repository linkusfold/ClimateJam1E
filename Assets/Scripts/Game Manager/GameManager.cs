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
        
        PlaceTower(selectedTower);
    }

    public void PlaceTower(Tower tower)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            Vector2Int gridPos = WorldToGrid(worldPos);
            GridCell cell = GetCell(gridPos);

            if (cell != null && !cell.isOccupied)
            {
                Instantiate(tower, GridToWorld(gridPos), Quaternion.identity);
                cell.Occupy();
            }
        }
    }
    
    #region Grid Logic
    [Header("Grid")]
    public GameObject cellPrefab;
    public int rows = 5;
    public int columns = 9;
    public float cellSize = 1f;
    
    public float xOffset = 0f;
    public float yOffset = 0f;

    private GridCell[,] grid;
    void GenerateGrid()
    {
        grid = new GridCell[columns, rows];

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 spawnPosition = new Vector3(x * cellSize, y * cellSize, 0f);
                GameObject cell = Instantiate(cellPrefab, spawnPosition, Quaternion.identity, transform);
                GridCell gridCell = cell.GetComponent<GridCell>();
                gridCell.gridPosition = new Vector2Int(x, y);
                grid[x, y] = gridCell;
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0f);
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
