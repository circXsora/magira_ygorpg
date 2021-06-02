/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/13 23:9:58
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    /// <summary>
    /// 战斗触发器
    /// </summary>
	public class BattleTrigger : MonoBehaviour 
	{
        public event EventHandler OnTouchedWithPlayer;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerLogic>();
            if (player != null)
            {
                OnTouchedWithPlayer?.Invoke(this, null);
            }
        }
    }
}