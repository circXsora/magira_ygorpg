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
        public CreatureView View { get; private set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }


        public CreatureLogic(CreatureInfo info)
        {
            this.info = info;
        }

        public Func<Task> LoadView;

        public virtual void SetView(CreatureView view)
        {
            if (this.View != null)
            {
                throw new InvalidOperationException($"{this} 禁止赋值已经存在view的logic");
            }
            this.View = view;
        }

        public virtual async Task Show()
        {
            await View.Show();
        }

        public virtual async Task Hide()
        {
            await View.Hide();
        }

        public async void DestroyView()
        {
            await View.Destroy();
        }

        public void SetPoint(EnvironmentContext.PointInfo pointInfo)
        {
            View.transform.position = pointInfo.transform.position;
            View.transform.rotation = pointInfo.transform.rotation;
        }
    }
}