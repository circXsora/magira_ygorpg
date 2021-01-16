using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BGMConfig))]
public class BGMConfigInspectorGUI : Editor
{
    public override void OnInspectorGUI()
    {
		serializedObject.Update();
		EditorList.Show(serializedObject.FindProperty("BGMList"), EditorListOption.None);
		serializedObject.ApplyModifiedProperties();
	}
}
