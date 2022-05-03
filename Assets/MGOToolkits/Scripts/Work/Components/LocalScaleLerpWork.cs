//------------------------------------------------------------------------------
//  <copyright file="LocalScaleLerpWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/7/8 13:50:28
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public class LocalScaleLerpWork : Work
    {
        public Transform Transform { get; private set; }
        public Vector3 Origin { get; private set; }
        public Vector3 Target { get; private set; }
        public float Time { get; private set; }
        private float _leftTime;

        public static LocalScaleLerpWork Create(Transform transform, Vector2 target, float time)
        {
            var work = ReferencePool.AcquireWithoutSpawn<LocalScaleLerpWork>() ?? new LocalScaleLerpWork();
            work.Transform = transform;
            work.Origin = transform.localScale;
            work.Target = target;
            work.Time = time;
            work._leftTime = time;
            return work;
        }

        protected override bool IsComplete => _leftTime <= 0;

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            base.UpdateCore(elapseSeconds, realElapseSeconds);
            _leftTime -= elapseSeconds;
            _leftTime = Mathf.Max(0, _leftTime);
            Transform.localScale = Vector3.Lerp(Origin, Target, (Time - _leftTime) / Time);
        }

        public override void Clear()
        {
            base.Clear();
            Transform = null;
            Origin = default;
            Target = default;
            Time = 0;
            _leftTime = 0;
        }
    }
}