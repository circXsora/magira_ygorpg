//------------------------------------------------------------------------------
//  <copyright file="MaterialComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/31 22:19:56
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
	public class FSMComponent : UnityGameFramework.Runtime.GameFrameworkComponent
	{
		//[SerializeField]
		//private GameObject battleFSM;

		[SerializeField]
		private GameObject procedureFSM;

        protected override void Awake()
        {
            base.Awake();
			RunProcedureFSM();
        }

   //     public void RunBattleFSM()
   //     {
			//battleFSM.gameObject.SetActive(true);
   //     }

		public void RunProcedureFSM()
        {
			procedureFSM.gameObject.SetActive(true);

		}
	}
}