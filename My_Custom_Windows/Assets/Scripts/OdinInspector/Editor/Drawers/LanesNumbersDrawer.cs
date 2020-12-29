using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class LanesNumbersDrawer : OdinValueDrawer<LanesNumbers> {

    protected override void DrawPropertyLayout(GUIContent label) {

        Rect rect = EditorGUILayout.GetControlRect();

        float rectWidth = 20f;

        rect.x += this.ValueEntry.SmartValue.indent;

        for (int i = 1; i <= 11; i++) {
            GUI.Label(rect, i.ToString());
            rect.x += rectWidth;
        }
    }

}
