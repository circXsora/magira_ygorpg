//------------------------------------------------------------------------------
//  <copyright file="ResourceComponentExtensions.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/19 22:11:22
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public static class ResourceComponentExtensions
    {
        public static void LoadSprite(this ResourceComponent resourceComponent, string spriteSimpleName, Action<Sprite> onLoaded)
        {
            resourceComponent.LoadAsset(AssetUtility.GetTextureAsset(spriteSimpleName), new GameFramework.Resource.LoadAssetCallbacks(
                (name, asset, duration, userData) =>
                {
                    var texture = asset as Texture2D;
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    onLoaded?.Invoke(sprite);
                }
                ));
        }
    }
}