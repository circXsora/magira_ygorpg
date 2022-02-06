//------------------------------------------------------------------------------
//  <copyright file="CreatureEntity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/21 15:40:19
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class CreatureEntity : Entity
    {
        public CreatureLogic CreatureLogic => Logic as CreatureLogic;

        private Renderer mainRenderer;
        public override Renderer MainRenderer
        {
            get
            {
                if (mainRenderer == null)
                {
                    mainRenderer = GetComponent<Renderer>();
                    if (mainRenderer == null)
                    {
                        mainRenderer = transform.Find("MainRenderer").GetComponent<Renderer>();
                    }
                }
                return mainRenderer;
            }
        }
    }
}