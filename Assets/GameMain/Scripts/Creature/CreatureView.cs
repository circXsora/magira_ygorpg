//------------------------------------------------------------------------------
//  <copyright file="CreatureEneity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:45:09
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
    public abstract class CreatureView : MonoBehaviour
    {
        public virtual async Task Show()
        {
            gameObject.SetActive(true);
        }
        public virtual async Task Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual async Task Destroy()
        {
            Destroy(gameObject);
        }
    }
}