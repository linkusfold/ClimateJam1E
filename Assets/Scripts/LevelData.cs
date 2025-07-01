using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public WaveData[] waves;
}
