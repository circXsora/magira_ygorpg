//------------------------------------------------------------------------------
//  <copyright file="LogicCreature.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:44:25
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using GameFramework.Event;
using MGO.Entity.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    [Serializable]
    public abstract class CreatureOldLogic
    {
        protected CreatureInfo info;
        public CreatureInfo Info => info;
        public CreatureView View { get; private set; }
        public virtual CreatureAI AI { get; set; }
        public CreatureState CreatureState { get; set; }
        public CreatureEntry EntryData { get; set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public bool Selectable { get => View.Selectable; set => View.Selectable = value; }

        public CreatureOldLogic(CreatureInfo info)
        {
            this.info = info;
            CreatureState = new CreatureState() { Hp = 50, MaxHp = 50 };
        }

        public virtual void SetView(CreatureView view)
        {
            Debug.Assert(view != null);
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

        public void SetPoint(PointInfo pointInfo)
        {
            if (View == null)
            {
                UberDebug.LogError("VIEW为空");
            }
            View.transform.position = pointInfo.transform.position;
            View.transform.rotation = pointInfo.transform.rotation;
        }
    }
}