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

namespace BBYGO
{
	public class LaunchProcedure : ProcedureBase
	{
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            UberDebug.LogChannel(nameof(LaunchProcedure),"OK");
        }
    }
}