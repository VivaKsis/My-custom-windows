using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DisableToggleOnAttribute))]
public class DisableToggleOnAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DisableToggleOnAttribute xCoordinate = (DisableToggleOnAttribute)attribute;

        position = new Rect(xCoordinate.xCoordinate, position.y, position.width, EditorGUI.GetPropertyHeight(property));

        EditorGUI.BeginProperty(position, label, property);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var valueRect = new Rect(position.x, position.y, 10, EditorGUI.GetPropertyHeight(property));

        var enabledToggle = property.FindPropertyRelative(Lanes.LanePiece.ENABLED_LABEL_TAG);

        if (enabledToggle.boolValue)
        {
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Lanes.LanePiece.VALUE_LABEL_TAG), GUIContent.none);
        }
        else
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Lanes.LanePiece.VALUE_LABEL_TAG), GUIContent.none);
            GUI.enabled = true;
        }
        
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 40f;
    }
}
