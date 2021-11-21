//------------------------------------------------------------------------------
//  <copyright file="EntityCOmponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/14 17:31:19
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO.Entity.Unity
{
    public class EntityComponent : MonoBehaviour, IEntityComponent
    {
        public Entity entity;

        public IEntity GetOwner()
        {
            return entity;
        }
    }
}