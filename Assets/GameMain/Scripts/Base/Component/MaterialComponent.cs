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

    public class MaterialComponent : GameFrameworkComponent
    {
        public Material OutlineMaterial;
        public Material DissolveMaterial;
    }

    public class MaterialChanger : EntityComponent
    {
        private Renderer entityRenderer;
        private Material origin;

        public void SetRenderer(Renderer entityrenderer)
        {
            this.entityRenderer = entityrenderer;
            origin = GameObject.Instantiate(entityrenderer.material);
        }

        public void ChangeTo(MaterialType materialType)
        {
            switch (materialType)
            {
                case MaterialType.Origin:
                    entityRenderer.material = origin;
                    break;
                case MaterialType.Outline:
                    entityRenderer.material = GameEntry.Material.OutlineMaterial;
                    break;
                case MaterialType.Dissolve:
                    entityRenderer.material = GameEntry.Material.DissolveMaterial;
                    break;
                default:
                    break;
            }
        }
    }
}