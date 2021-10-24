//------------------------------------------------------------------------------
//  <copyright file="HPBarItem.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/24 10:49:55
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BBYGO
{
	public class HPBarItem : UIItem
	{
		public float HPAnimationTime = 0.3f;
		public Slider hpSlider;
		public TMPro.TMP_Text hpText;

		public void ShowHP(int Hp, int MaxHp)
        {
			float ratio = Hp / (float)MaxHp;
			hpSlider.DOValue(ratio, HPAnimationTime);
			hpText.text = $"{Hp}/{MaxHp}";
        }
	}
}