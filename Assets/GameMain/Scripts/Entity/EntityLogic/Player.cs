using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    /// <summary>
    /// 玩家
    /// </summary>
    public class Player : UniversalEntityLogic
    {

        private float _speed;

        private Vector2 _velocity;
        private Rigidbody2D _body;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            PlayerData playerData = userData as PlayerData;
            _speed = playerData.Speed;
            Log.Info("=========速度为" + _speed);
            Name = Utility.Text.Format("Player ({0})", Id.ToString());
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            _velocity.x = Input.GetAxis("Horizontal");
            _velocity.y = Input.GetAxis("Vertical");

            if (_velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            _animator.SetBool("Run", _velocity.sqrMagnitude > 0);

            _body.velocity = _velocity * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Log.Info("与" + collision.name + "碰撞");
            var entity = collision.GetComponent<UniversalEntityLogic>();
            if (entity is Room)
            {
                GameEntry.Event.Raise(this, PlayerArrivedRoomEventArgs.Create(entity as Room));
            }
        }
    }
}
