/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/5 17:57:8
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BBYGO
{
    public static class SpriteExporter
    {

        [MenuItem("Tools/导出Sprites")]
        static void SaveSprite()
        {
            string resourcesPath = "Assets/Resources/";
            foreach (Object obj in Selection.objects)
            {
                string selectionPath = AssetDatabase.GetAssetPath(obj);
                // 必须最上级是"Assets/Resources/"
                if (selectionPath.StartsWith(resourcesPath))
                {
                    string selectionExt = Path.GetExtension(selectionPath);
                    if (selectionExt.Length == 0)
                    {
                        continue;
                    }
                    // 从路径"Assets/Resources/UI/testUI.png"得到路径"UI/testUI"
                    string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                    loadPath = loadPath.Substring(resourcesPath.Length);
                    // 加载此文件下的所有资源
                    Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
                    if (sprites.Length > 0)
                    {
                        // 创建导出文件夹
                        string outPath = Application.dataPath + "/Resources/" + loadPath;
                        Directory.CreateDirectory(outPath);
                        foreach (Sprite sprite in sprites)
                        {
                            // 创建单独的纹理
                            Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, sprite.texture.format, false);
                            tex.SetPixels(sprite.texture.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin,
                            (int)sprite.rect.width, (int)sprite.rect.height));
                            tex.Apply();
                            // 写入成PNG文件
                            File.WriteAllBytes(outPath + "/" + sprite.name + ".png", tex.EncodeToPNG());
                        }
                        Debug.Log("SaveSprite to " + outPath);
                    }
                }
            }
            Debug.Log("SaveSprite Finished");
        }
    }
}