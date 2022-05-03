//------------------------------------------------------------------------------
//  <copyright file="InputComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/4/9 19:02:02
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class InputComponent : GameComponent
    {
        public event Action<Vector2> OnGetMouseButton;
        public event Action<Vector2> OnGetMouseButtonUp;

        //private IInputManager inputManager;

        //private void Start()
        //{
        //    inputManager = GameEntry.GameModule.GetInstance<InputManager>();
        //}

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, Cam, out Vector2 localPosition);
                OnGetMouseButton?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, Cam, out Vector2 localPosition);
                OnGetMouseButtonUp?.Invoke(Input.mousePosition);
            }
        }
    }
}