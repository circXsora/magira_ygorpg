//------------------------------------------------------------------------------
//  <copyright file="LogicCreature.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:44:25
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
    public abstract class CreatureLogic
    {
        private CreatureInfo info;
        protected CreatureView view;

        public Vector3 Position { get; internal set; }
        public Quaternion Rotation { get; internal set; }


        public CreatureLogic(CreatureInfo info)
        {
            this.info = info;
        }

        public virtual async Task LoadView()
        {
            var instance = UnityEngine.Object.Instantiate( await GameEntry.Resource.LoadAsync<GameObject>("CreatureTemplates/" + info.type.ToString()), GameEntry.Creatures.transform);
            view = instance.GetOrAddComponent<PlayerView>();
        }

        public virtual async Task Show()
        {
            await view.Show();
        }

        public virtual async Task Hide()
        {
            await view.Hide();
        }

        internal async void DestroyView()
        {
            await view.Destroy();
        }

        internal void SetPoint(EnvironmentContext.PointInfo pointInfo)
        {
            view.transform.position = pointInfo.transform.position;
            view.transform.rotation = pointInfo.transform.rotation;
        }
    }
}