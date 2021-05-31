//------------------------------------------------------------------------------
//  <copyright file="CameraLogic.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/31 22:13:08
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
	public class MazeCameraLogic : EntityLogic
	{
        public Transform Target
        {
            get; set;
        }

        public float MoveSpeed = 10;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            GameEntry.Event?.Subscribe(PlayerArrivedRoomEventArgs.EventId, OnPlayerArrivedRoom);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            GameEntry.Event?.Unsubscribe(PlayerArrivedRoomEventArgs.EventId, OnPlayerArrivedRoom);
            base.OnHide(isShutdown, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (Target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(Target.position.x, Target.position.y, transform.position.z), MoveSpeed * elapseSeconds);
            }
        }

        private void OnPlayerArrivedRoom(object sender, GameEventArgs e)
        {
            var ne = e as PlayerArrivedRoomEventArgs;
            Target = ne.Room.CachedTransform;
        }
    }
}