//------------------------------------------------------------------------------
//  <copyright file="MenuForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:11:24
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using MGO;
namespace BBYGO
{
    public class MenuForm : SoraUIForm
    {
        public Button StartButton;
        public Button EndButton;
        public RawImage BgImage;



        private void Awake()
        {
            StartButton.onClick.AddListener(() =>
           {
               GameEntry.Event.Raise(this, GameStartButtonClickedEventArgs.Create());
           });
            EndButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        public override async Task Show()
        {
            var bgNames = Enum.GetValues(typeof(BgType));
            var bg = await GameEntry.Resource.LoadBg((BgType)bgNames.Random1());
            BgImage.texture = bg;

            await base.Show();
        }
    }
}