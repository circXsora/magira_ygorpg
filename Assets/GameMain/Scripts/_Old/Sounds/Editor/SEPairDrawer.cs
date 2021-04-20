using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SEPair), true)]
public class SEPairDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        var oldIndentLevel = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        //if (position.height > 16f)
        //{
        //    position.height = 16f;
        //    EditorGUI.indentLevel += 1;
        //    contentPosition = EditorGUI.IndentedRect(position);
        //    contentPosition.y += 18f;
        //}

        contentPosition.width *= .35f;
        EditorGUI.indentLevel = 0;
        var namePro = property.FindPropertyRelative("Name");
        EditorGUI.LabelField(contentPosition, namePro.enumDisplayNames[namePro.enumValueIndex]);
        //EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Name"), GUIContent.none);

        contentPosition.x += contentPosition.width;
        contentPosition.width *= 1.5f;
        EditorGUI.indentLevel = 1;
        EditorGUIUtility.labelWidth = 14f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("Clip"), GUIContent.none);

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = oldIndentLevel;
    }

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    return Screen.width / Screen.dpi < 3.35 ? (16f + 18f) : 16f;
    //}
}
