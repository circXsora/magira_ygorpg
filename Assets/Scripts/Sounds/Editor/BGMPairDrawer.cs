using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BGMPair), true)]
public class BGMPairDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var oldIndentLevel = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        contentPosition.width *= .35f;
        EditorGUI.indentLevel = 0;
        var namePro = property.FindPropertyRelative("Name");
        EditorGUI.LabelField(contentPosition, namePro.enumDisplayNames[namePro.enumValueIndex]);

        contentPosition.x += contentPosition.width;
        contentPosition.width *= 1.5f;
        EditorGUI.indentLevel = 1;
        EditorGUIUtility.labelWidth = 14f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Clip"), GUIContent.none);

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = oldIndentLevel;
    }
}
