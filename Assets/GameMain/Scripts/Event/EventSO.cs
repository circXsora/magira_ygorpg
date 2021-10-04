//------------------------------------------------------------------------------
//  <copyright file="EventSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/9/28 23:37:11
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
	[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/EventSO")]
	public class EventSO : ScriptableObject
	{

		private EventHandler<object> OnRaise;

		public void AddListener(EventHandler<object> eventHandler)
        {
			OnRaise += eventHandler;
        }

		public void RemoveListener(EventHandler<object> eventHandler)
		{
			OnRaise -= eventHandler;
		}

		public void Raise(object sender, object data)
        {
			// UberDebug.LogChannel("Event",name + "事件被触发了");
			OnRaise?.Invoke(sender, data);
		}
	}
}