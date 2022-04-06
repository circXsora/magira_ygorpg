//------------------------------------------------------------------------------
//  <copyright file="SoraUIForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:11:55
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using DG.Tweening;
using MGO;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
	public enum BgType
	{
		Bg1,
		Bg2,
		Bg3
	}

	public class UIForm : SerializedMonoBehaviour
	{
		public float ShowAnimationTime = 0.8f;
		public float HideAnimationTime = 0.8f;

		public virtual async Task Show()
        {
			var cg = gameObject.GetOrAddComponent<CanvasGroup>();
			await cg.DOFade(1, ShowAnimationTime).From(0).AsyncWaitForCompletion();
			cg.blocksRaycasts = true;
			cg.interactable = true;
		}

		public virtual async Task Hide()
        {
			var cg = gameObject.GetOrAddComponent<CanvasGroup>();
			cg.blocksRaycasts = false;
			cg.interactable = false;
			await cg.DOFade(0, HideAnimationTime).AsyncWaitForCompletion();
		}
	}
}