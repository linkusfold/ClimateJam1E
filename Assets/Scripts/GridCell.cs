using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool isOccupied = false;

    public void Occupy()
    {
        isOccupied = true;
        // You can add animation/visual changes here
    }

    public void Vacate()
    {
        isOccupied = false;
    }
}