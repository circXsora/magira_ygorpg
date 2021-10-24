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
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class MonsterView : CreatureView
    {
        public MonsterUI MonsterUI { get; set; }
        
        public HPBarItem HPBar => MonsterUI.HPBar;
    }
}