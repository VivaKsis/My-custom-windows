using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : OdinEditor {
    public override void OnInspectorGUI() {
        LevelData levelData = (LevelData)target;
        float xPosition = levelData.levelDataEditorWindowCurrentSize.width / 2;

        GUI.Label(new Rect(xPosition, 35, 500, 20), "Mission Totals", EditorStyles.boldLabel);
        GUI.Label(new Rect(xPosition, 60, 500, 20), "Unit count: " + levelData.unitCount.ToString(), EditorStyles.boldLabel);
        GUI.Label(new Rect(xPosition, 80, 500, 20), "Obstacles count: " + levelData.obstaclesCount.ToString(), EditorStyles.boldLabel);
        GUI.Label(new Rect(xPosition, 100, 500, 20), "Item count: " + levelData.itemCount.ToString(), EditorStyles.boldLabel);
        GUI.Label(new Rect(xPosition, 120, 500, 20), "Bosses: " + levelData.bosses.ToString(), EditorStyles.boldLabel);
        GUI.Label(new Rect(xPosition, 140, 500, 20), "Approx. Mission Time: " + levelData.approxMisssionTime, EditorStyles.boldLabel);

        base.OnInspectorGUI();
    }
}
