//------------------------------------------------------------------------------
//  <copyright file="MaterialComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/31 22:19:56
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using MGO.Entity.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{

    public enum MaterialType
    {
        Origin,
        Outline,
        Dissolve,
    }

    public class MaterialComponent : GameComponent
    {
        public Material OutlineMaterial;
        public Material DissolveMaterial;
    }
}