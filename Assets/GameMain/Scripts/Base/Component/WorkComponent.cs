//------------------------------------------------------------------------------
//  <copyright file="WorkComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/4/9 18:07:27
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class WorkComponent : GameComponent
	{
		private IWorkManager workManager;

        private void Start()
        {
            workManager = GameEntry.GameModule.GetInstance<WorkManager>();
        }
    }
}