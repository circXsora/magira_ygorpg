//------------------------------------------------------------------------------
//  <copyright file="InitProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/20 23:41:25
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class MenuProcedure : ProcedureBase
    {
        MainMenuForm menuForm;

        public override async void OnEnter()
        {
            menuForm = await GameEntry.UI.Get(UIType.MenuForm) as MainMenuForm;
            menuForm.StartButton.onClick.AddListener(OnStart);
            menuForm.EndButton.onClick.AddListener(OnEnd);
            _ = menuForm.Show();
            base.OnEnter();
        }

        public override void OnLeave(bool isShutdown)
        {
            menuForm.StartButton.onClick.RemoveListener(OnStart);
            menuForm.EndButton.onClick.RemoveListener(OnEnd);
            _ = menuForm.Hide();
            base.OnLeave(isShutdown);
        }

        private void OnStart()
        {
            Next();
        }

        private void OnEnd()
        {
            Application.Quit();
        }
    }
}