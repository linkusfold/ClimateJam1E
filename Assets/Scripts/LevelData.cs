using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public float countdown = 5f;
    public int maxEnemiesSafe = 4;
    public WaveData[] waves;
}
