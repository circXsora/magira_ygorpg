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
using MGO;
namespace BBYGO
{
    /// <summary>
    /// 主菜单
    /// </summary>
    public class MainMenuForm : UIForm
    {
        public Button StartButton;
        public Button EndButton;
        public RawImage BgImage;

        protected override async Task ShowCore()
        {
            var bgNames = Enum.GetValues(typeof(BgType));
            var bg = await GameEntry.Resource.LoadBg((BgType)bgNames.Random1());
            BgImage.texture = bg;

            await base.ShowCore();
        }
    }
}