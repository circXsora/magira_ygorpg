//------------------------------------------------------------------------------
//  <copyright file="LaunchProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/4 23:05:37
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BBYGO.SoraProcedureComponent;

namespace BBYGO
{
	public class MenuProcedure : SoraProcedureBase
    {
        public override void OnInit()
        {
            base.OnInit();
            UberDebug.Log("Menu Init");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            UberDebug.Log("Menu Start");
            GameEntry.UI.Open(UIType.MenuForm);
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate( elapseSeconds, realElapseSeconds);
        }

        public override void OnLeave(bool isShutdown)
        {
            GameEntry.UI.Close(UIType.MenuForm);
            base.OnLeave(isShutdown);
        }
    }
}