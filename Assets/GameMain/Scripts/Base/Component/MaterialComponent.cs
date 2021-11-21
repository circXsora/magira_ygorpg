//------------------------------------------------------------------------------
//  <copyright file="MaterialComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/31 22:19:56
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class MaterialComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        public Material OutlineMaterial;
        public Material DissolveMaterial;

        public enum MaterialType
        {
            Origin,
            Outline,
            Dissolve,
        }

        public class MaterialChanger
        {
            private Renderer renderer;
            private Material origin;

            public MaterialChanger(Renderer renderer)
            {
                this.renderer = renderer;
                origin = Instantiate(renderer.material);
            }

            public void ChangeTo(MaterialType materialType)
            {
                switch (materialType)
                {
                    case MaterialType.Origin:
                        renderer.material = origin;
                        break;
                    case MaterialType.Outline:
                        renderer.material = GameEntry.Material.OutlineMaterial;
                        break;
                    case MaterialType.Dissolve:
                        renderer.material = GameEntry.Material.DissolveMaterial;
                        break;
                    default:
                        break;
                }
            }
        }

        public MaterialChanger GetMaterialChanger(Renderer renderer)
        {
            return new MaterialChanger(renderer);
        }
    }
}