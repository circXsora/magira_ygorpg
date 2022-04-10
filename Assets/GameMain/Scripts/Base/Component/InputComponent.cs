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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class InputComponent : GameComponent
	{
        private IInputManager inputManager;

        private void Start()
        {
            inputManager = GameEntry.GameModule.GetInstance<InputManager>();
        }
    }
}