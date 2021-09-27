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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public abstract class CreatureLogic
    {
        protected CreatureInfo info;
        public CreatureView View { get; private set; }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }


        public CreatureLogic(CreatureInfo info)
        {
            this.info = info;
            GameEntry.Event.Subscribe(CreatureViewBeClickedEventArgs.EventId, OnViewBeClicked);
            GameEntry.Event.Subscribe(CreatureViewBeEnteredEventArgs.EventId, OnViewBeEntered);
            GameEntry.Event.Subscribe(CreatureViewBeExitedEventArgs.EventId, OnViewBeExited);
        }

        ~CreatureLogic()
        {
            GameEntry.Event.Unsubscribe(CreatureViewBeClickedEventArgs.EventId, OnViewBeClicked);
            GameEntry.Event.Unsubscribe(CreatureViewBeEnteredEventArgs.EventId, OnViewBeEntered);
            GameEntry.Event.Unsubscribe(CreatureViewBeExitedEventArgs.EventId, OnViewBeExited);
        }


        private void OnViewBeClicked(object sender, GameEventArgs e)
        {
            OnMyViewBeClicked(e as CreatureViewBeExitedEventArgs);
        }

        protected virtual void OnMyViewBeClicked(CreatureViewBeExitedEventArgs e)
        {

        }

        private void OnViewBeEntered(object sender, GameEventArgs e)
        {
            OnMyViewBeEntered(e as CreatureViewBeExitedEventArgs);
        }

        protected virtual void OnMyViewBeEntered(CreatureViewBeExitedEventArgs e)
        {

        }

        private void OnViewBeExited(object sender, GameEventArgs e)
        {
            if (sender.Equals(View))
            {
                OnMyViewBeExited(e as CreatureViewBeExitedEventArgs);
            }
        }

        protected virtual void OnMyViewBeExited(CreatureViewBeExitedEventArgs e)
        {

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