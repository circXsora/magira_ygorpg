//------------------------------------------------------------------------------
//  <copyright file="CreatureVisualEffectConfig.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/12 22:20:36
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
	[CreateAssetMenu(fileName = "NewObj", menuName = "ScriptableObjects/CreatureVisualEffectConfig")]
	public class CreatureVisualEffectConfigSO : SerializedScriptableObject
	{
		public Dictionary<VisualEffectTypeSO, VisualEffectParam> effectParams = new Dictionary<VisualEffectTypeSO, VisualEffectParam>();
	}
}