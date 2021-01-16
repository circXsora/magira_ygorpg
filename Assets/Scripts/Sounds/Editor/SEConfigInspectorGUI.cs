using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SEConfig))]
public class SEConfigInspectorGUI : Editor
{
    public override void OnInspectorGUI()
    {
		serializedObject.Update();
		EditorList.Show(serializedObject.FindProperty("SEList"), EditorListOption.None);
		serializedObject.ApplyModifiedProperties();
	}
}
