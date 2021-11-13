//------------------------------------------------------------------------------
//  <copyright file="LaunchState.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/13 11:43:28
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
	public class LaunchState : FSMState
	{
        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Finish();
        }

        protected override void OnExit()
        {
            base.OnExit();
        }
    }
}