//------------------------------------------------------------------------------
//  <copyright file="VisualEffectTypeSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/11 21:55:29
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	[Serializable]
    public enum BaseVisualEffectType
    {
		Attack,
		Defend,
		SKill,
		SufferDamage,
		Escape,
    }

	[CreateAssetMenu(fileName = "NewObj", menuName = "ScriptableObjects/VisualEffectTypeSO")]
	public class VisualEffectTypeSO : SerializedScriptableObject
	{
		public BaseVisualEffectType baseType;
	}
}