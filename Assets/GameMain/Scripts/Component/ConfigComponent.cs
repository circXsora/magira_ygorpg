//------------------------------------------------------------------------------
//  <copyright file="CreaturesComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:57:05
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class ConfigComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        [Serializable]
        public class SpriteConfig
        {
            [SerializeField]
            private SpriteConfigSO monsterSpriteConfig;

            public Sprite GetMonsterSprite(int id) => monsterSpriteConfig.sprites[id];
        }
        public SpriteConfig sprite;
    }
}