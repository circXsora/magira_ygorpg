//------------------------------------------------------------------------------
//  <copyright file="ProcudureBaseSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:13:21
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	[CreateAssetMenu(fileName = "Procudure", menuName = "ScriptableObjects/Procudure")]
	public abstract class ProcedureBaseSO : ScriptableObject
	{
		public string ProcedureName;

        /// <summary>
        /// 状态初始化时调用。
        /// </summary>
        public virtual void OnInit()
        {
            
        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        public virtual void OnEnter()
        {
            
        }

        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            
        }

        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        public virtual void OnLeave(bool isShutdown)
        {
           
        }

        /// <summary>
        /// 状态销毁时调用。
        /// </summary>
        public virtual void OnDestroy()
        {
           
        }
    }
}