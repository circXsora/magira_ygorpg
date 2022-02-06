//------------------------------------------------------------------------------
//  <copyright file="EnvironmentContext.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 18:50:22
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class EnvironmentBindings : SerializedMonoBehaviour
    {
        [SerializeField]
        private GameObject EnvironmentObject;
        [SerializeField]
        private PointInfo[] playerPointInfos;
        [SerializeField]
        private PointInfo[] playerMonsterPointInfos;
        [SerializeField]
        private PointInfo[] enemyPointInfos;

        public PointInfo GetPlayerMonsterPoint(int index)
        {
            var point = playerMonsterPointInfos[index];
            Debug.Assert(point != null);
            return point;
        }

        public PointInfo GetPlayerPoint(int index)
        {
            var point = playerPointInfos[index];
            Debug.Assert(point != null);
            return point;
        }

        public PointInfo GetEnemyPoint(int index)
        {
            var point = enemyPointInfos[index];
            Debug.Assert(point != null);
            return point;
        }
    }
}