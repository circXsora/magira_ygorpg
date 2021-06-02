using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    /// <summary>
    /// 玩家
    /// </summary>
    public class BattleFieldPlayerLogic : UniversalEntityLogic
    {

        private Rigidbody2D _body;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            transform.localPosition = Vector3.zero;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            var data = userData as BattleFieldPlayerData;
            GameEntry.Entity.AttachEntity(Entity, data.OwnerId, data.PointName);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }
    }
}
