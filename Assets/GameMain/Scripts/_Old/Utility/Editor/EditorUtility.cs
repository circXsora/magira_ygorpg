using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//public static class EditorUtility
//{
//    [MenuItem("Tools/当前代码行数")]
//    private static void PrintTotalLine()
//    {
//        string[] fileName = AssetDatabase.FindAssets("t:Script", new string[] { "Assets/GameMain/Scripts" });

//        int totalLine = 0;
//        foreach (var temp in fileName)
//        {
//            int nowLine = 0;
//            string path = AssetDatabase.GUIDToAssetPath(temp);
//            StreamReader sr = new StreamReader(path);
//            //while (!string.IsNullOrEmpty(sr.ReadLine()))
//            while (sr.ReadLine() != null)
//            {
//                nowLine++;
//            }

//            //文件名+文件行数
//            Debug.Log(String.Format("{0}——{1}", path, nowLine));
//            totalLine += nowLine;
//        }

//        Debug.Log(String.Format("总代码行数：{0}", totalLine));
//    }
//}
