//------------------------------------------------------------------------------
//  <copyright file="BattleCameraLogic.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/6/1 20:19:50
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
	public class BattleCameraLogic : EntityLogic
	{
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            GameEntry.Scene.RegiseterBattleCamera(GetComponent<Camera>());
        }
    }
}