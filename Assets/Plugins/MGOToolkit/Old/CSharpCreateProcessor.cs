/****************************************************
 *  Copyright © #CREATEYEAR #AUTHORNAME#. All rights reserved.
 *------------------------------------------------------------------------
 *  文件：CSharpCreator#FILEEXTENSION#
 *  作者：circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期：Created by #SMARTDEVELOPERS# on #CREATETIME#
 *  项目：#PROJECTNAME#
 *  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//namespace MGO
//{
//	public class CSharpCreateProcessor : UnityEditor.AssetModificationProcessor
//	{
//        public const string authorName = "circXsora";

//        private static void OnWillCreateAsset(string path)
//        {
//            path = path.Replace(".meta", "");
//            if (path.EndsWith(".cs"))
//            {
//                string str = File.ReadAllText(path);
//                str = str.Replace("#SCRIPTNAME#", Path.GetFileName(Path.GetFileNameWithoutExtension(path)));
//                str = str.Replace("#AUTHORNAME#", authorName);
//                str = str.Replace("#FILEEXTENSION#", path.Substring(path.LastIndexOf(".")));
//                str = str.Replace("Plane", Environment.UserName);
//                str = str.Replace("#SMARTDEVELOPERS#", PlayerSettings.companyName);
//                str = str.Replace("#CREATETIME#", string.Concat(DateTime.Now.ToString("d"), " ", DateTime.Now.Hour, ":", DateTime.Now.Minute, ":", DateTime.Now.Second));
//                str = str.Replace("#CREATEYEAR#", DateTime.Now.Year.ToString());
//                str = str.Replace("#PROJECTNAME#", "BBYGO");
//                File.WriteAllText(path, str);
//                AssetDatabase.ImportAsset(path);
//            }
//        }
//	}
//}