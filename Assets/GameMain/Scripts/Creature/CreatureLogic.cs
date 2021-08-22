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

        public Func<Task> LoadView;

        public void SetView(CreatureView view)
        {
            if (this.view != null)
            {
                throw new InvalidOperationException($"{this} 禁止赋值已经存在view的logic");
            }
            this.view = view;
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