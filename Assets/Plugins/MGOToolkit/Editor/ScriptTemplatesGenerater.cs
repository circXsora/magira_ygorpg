/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/4 12:57:58
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BBYGO
{
    public static class ScriptTemplatesGenerater
    {

        public enum ScriptType
        {
            Entity,
            EntityData,
            EventArgs
        }

        //模板文件位置                                               
        static string tempEntityDataPath = Application.dataPath + "/Plugins/MGOToolkit/Editor/EntityDataTemplate.txt";
        static string tempEntityPath = Application.dataPath + "/Plugins/MGOToolkit/Editor/EntityTemplate.txt";
        static string tempEventArgsPath = Application.dataPath + "/Plugins/MGOToolkit/Editor/EventArgsTemplate.txt";

        [MenuItem("Assets/Create/Scripts/New Entity Data", false, 80)]
        static void CreateNewEntityData()
        {
            ScriptGenerate(ScriptType.EntityData);
        }

        [MenuItem("Assets/Create/Scripts/New Entity", false, 80)]
        static void CreateNewEntity()
        {
            ScriptGenerate(ScriptType.Entity);
        }

        [MenuItem("Assets/Create/Scripts/New Event Args", false, 80)]
        static void CreateNewEventArgs()
        {
            ScriptGenerate(ScriptType.EventArgs);
        }

        private static void ScriptGenerate(ScriptType scriptType)
        {
            try
            {
                switch (scriptType)
                {
                    case ScriptType.Entity:
                        ProjectWindowUtil.CreateAssetWithContent("NewEntity.cs", File.ReadAllText(tempEntityPath), EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);
                        break;
                    case ScriptType.EntityData:
                        ProjectWindowUtil.CreateAssetWithContent("NewEntityData.cs", File.ReadAllText(tempEntityDataPath), EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);
                        break;
                    case ScriptType.EventArgs:
                        ProjectWindowUtil.CreateAssetWithContent("NewEventArgs.cs", File.ReadAllText(tempEventArgsPath), EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("模板文件路径错误!!! " + ex.Message);
            }
        }
    }
}