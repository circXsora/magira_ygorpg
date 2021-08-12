//------------------------------------------------------------------------------
//  <copyright file="BattleContextSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/11 23:29:15
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BBYGO
{
	[CreateAssetMenu(fileName = "BattleContext", menuName = "ScriptableObjects/BattleContextSO")]
	public class BattleContextSO : SerializedScriptableObject
	{
		public Dictionary<string, GameObject> BattleMonsters = new Dictionary<string, GameObject>();
	}
}