//------------------------------------------------------------------------------
//  <copyright file="TextureConfigSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/4 12:02:14
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	[CreateAssetMenu(fileName = "NewObj", menuName = "ScriptableObjects/SpriteConfigSO")]
	public class SpriteConfigSO : ScriptableObject
	{
		public List<Sprite> sprites;
	}
}