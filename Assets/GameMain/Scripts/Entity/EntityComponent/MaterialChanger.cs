//------------------------------------------------------------------------------
//  <copyright file="MaterialChanger.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/5/3 22:41:52
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{

    public class MaterialChanger : EntityGear
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