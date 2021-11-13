//------------------------------------------------------------------------------
//  <copyright file="MainMenuState.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/13 11:51:16
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    [Category("Game Procedure/Main")]
    public class MainMenuState : FSMState
	{
        protected async override void OnEnter()
        {
            await GameEntry.UI.Open(UIType.MenuForm);
        }

        protected async override void OnExit()
        {
            await GameEntry.UI.Close(UIType.MenuForm);
        }
    }
}