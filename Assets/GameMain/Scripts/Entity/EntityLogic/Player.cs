//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    /// <summary>
    /// 玩家
    /// </summary>
    public class Player : Entity
    {
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            Name = Utility.Text.Format("Player ({0})", Id.ToString());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Log.Info("与" + collision.name + "碰撞");
            var entity = collision.GetComponent<Entity>();
            if (entity is Room)
            {
                GameEntry.Event.Fire(this, PlayerArrivedRoomEventArgs.Create(entity as Room));
            }
        }
    }
}
