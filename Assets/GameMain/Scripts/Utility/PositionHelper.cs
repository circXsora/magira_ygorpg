//------------------------------------------------------------------------------
//  <copyright file="PositionHelper.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/29 18:13:33
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public static class PositionHelper
	{
		public static Vector2 WorldPos2CanvasPos(Vector3 worldPos)
        {
			//first you need the RectTransform component of your canvas
			RectTransform CanvasRect = GameEntry.UI.MainCanvas.GetComponent<RectTransform>();

			//then you calculate the position of the UI element
			//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

			Vector2 ViewportPosition = GameEntry.MainCamera.WorldToViewportPoint(worldPos);
			Vector2 WorldObject_ScreenPosition = new Vector2(
			((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

			//now you can set the position of the ui element
			return WorldObject_ScreenPosition;
		}
	}
}