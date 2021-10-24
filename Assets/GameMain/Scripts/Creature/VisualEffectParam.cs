//------------------------------------------------------------------------------
//  <copyright file="VisualEffectParamSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/12 22:22:53
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace BBYGO
{

    [Serializable]
    public class VisualEffectParam
    {
        [Serializable]
        public class TimePoint
        {
            [Range(0, 1)]
            public float startEffectTimePoint;
            [Range(0, 1)]
            public float realEffectTimePoint;
            [Range(0, 1)]
            public float endEffectTimePoint;
        }

        public class SpecialEffectParam
        {
            [Range(0, 1)]
            public float showTimePoint;

            public GameObject EffectTempalte;

            public Vector3 offsetFromSelf;
        }

        [LabelText("总时间")]
        public float totalTime;

        [LabelText("效果时间点")]
        public List<TimePoint> timePoints;

        [LabelText("特效")]
        public List<SpecialEffectParam> specialEffectParams;

    }
}