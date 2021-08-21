//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class CreatureFactory
    {
        internal CreatureLogic Create(CreatureInfo info)
        {
            switch (info.type)
            {
                case CreaturesType.Monster:
                    return new MonsterLogic(info);
                case CreaturesType.Player:
                    return new PlayerLogic(info);
                default:
                    return null;
            }
        }
    }
}