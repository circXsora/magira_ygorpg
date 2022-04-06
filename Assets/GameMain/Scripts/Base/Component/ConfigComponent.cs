//------------------------------------------------------------------------------
//  <copyright file="CreaturesComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:57:05
//  项目:  邦邦游戏王
//  功能:  用于管理一切静态数据
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class ConfigComponent : GameComponent
    {
        [Serializable]
        public class SpriteConfig
        {
            [SerializeField]
            private SpriteConfigSO monsterSpriteConfig;

            public Sprite GetMonsterSprite(int id) => monsterSpriteConfig.sprites[id];
        }

        [Serializable]
        public class VisualEffectTypeConfig
        {
            public VisualEffectTypeSO normalAttack1;
            public VisualEffectTypeSO normalDefend1;
            public VisualEffectTypeSO normalSufferDamage1;
            public VisualEffectTypeSO normalSkill1;
            public VisualEffectTypeSO normalEscape1;
        }

        public SpriteConfig sprite;
        public VisualEffectTypeConfig visualEffectType;
    }
}