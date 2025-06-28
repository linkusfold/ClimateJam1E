using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {

        GameManager gameManager = (GameManager)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Destroy all enemies"))
        {
            gameManager.DestroyAllEnemies();
        }
    }
}
