//------------------------------------------------------------------------------
//  <copyright file="EntitySelection.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/26 23:38:24
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class UniversalEntitySelector : EntitySelector
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            //GameEntry.Event.OnEntityPointerClick?.Raise(GetOwner(), eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            //GameEntry.Event.OnEntityPointerEnter?.Raise(GetOwner(), eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            //GameEntry.Event.OnEntityPointerExit?.Raise(GetOwner(), eventData);
        }
    }
}