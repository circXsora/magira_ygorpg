//------------------------------------------------------------------------------
//  <copyright file="AnchoredPositionLerpWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/7/7 16:19:07
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public class AnchoredPositionLerpWork : Work
    {
        public RectTransform RectTransform { get; private set; }
        public Vector2 Origin { get; private set; }
        public Vector2 Target { get; private set; }
        public float Time { get; private set; }
        private float _leftTime;

        public static AnchoredPositionLerpWork Create(RectTransform rectTransform, Vector2 target, float time)
        {
            var work = GameFramework.ReferencePool.AcquireWithoutSpawn<AnchoredPositionLerpWork>() ?? new AnchoredPositionLerpWork();
            work.RectTransform = rectTransform;
            work.Origin = rectTransform.anchoredPosition;
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
            RectTransform.anchoredPosition = Vector2.LerpUnclamped(Origin, Target, (Time - _leftTime) / Time);
        }

        public override void Clear()
        {
            base.Clear();
            RectTransform = null;
            Origin = default;
            Target = default;
            Time = 0;
            _leftTime = 0;
        }
    }
}