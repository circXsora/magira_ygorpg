//------------------------------------------------------------------------------
//  <copyright file="InitProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/20 23:41:25
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class InitProcedure : ProcedureBase
	{
        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            Next();
        }
    }
}