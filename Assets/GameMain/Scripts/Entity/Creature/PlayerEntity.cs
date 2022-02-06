//------------------------------------------------------------------------------
//  <copyright file="BattleContextSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/11 23:29:15
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace BBYGO
{
    public class PlayerEntity : CreatureEntity
    {
        public PlayerLogic PlayerLogic => Logic as PlayerLogic;
    }
}