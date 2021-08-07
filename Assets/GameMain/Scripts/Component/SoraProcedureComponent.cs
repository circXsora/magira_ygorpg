//------------------------------------------------------------------------------
//  <copyright file="SoraProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 12:08:27
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class SoraProcedureBase
    {
        /// <summary>
        /// 初始化有限状态机状态基类的新实例。
        /// </summary>
        public SoraProcedureBase()
        {
        }

        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        public virtual void OnInit()
        {
        }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
        public virtual void OnLeave(bool isShutdown)
        {
        }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        public virtual void OnDestroy()
        {
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        public void ChangeState<TState>() where TState : SoraProcedureBase
        {
            GameEntry.Procedure.ChangeState<TState>();
        }
    }

    public class SoraProcedureComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {


        public SoraProcedureBase CurrentProcedure { get; private set; }
        public readonly SoraProcedureBase InitProcedure = new A_LaunchProcedure();
        private Dictionary<string, SoraProcedureBase> _procedureDic = new Dictionary<string, SoraProcedureBase>();


        private void Start()
        {
            _procedureDic.Add(InitProcedure.GetType().Name, InitProcedure);
        }

        private void OnEnable()
        {
            CurrentProcedure = InitProcedure;
            InitProcedure.OnInit();
            InitProcedure.OnEnter();
        }

        private void Update()
        {
            CurrentProcedure.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void OnDisable()
        {
            CurrentProcedure.OnLeave(true);
        }

        public void ChangeState<TState>() where TState : SoraProcedureBase
        {
            if (_procedureDic.TryGetValue(nameof(TState), out var state))
            {
                CurrentProcedure.OnLeave(false);
                CurrentProcedure = state;
                CurrentProcedure.OnEnter();
            }
            else
            {
                var newState = (SoraProcedureBase)Activator.CreateInstance<TState>();
                newState.OnInit();
                newState.OnEnter();
                _procedureDic.Add(newState.GetType().Name, newState);
                CurrentProcedure = newState;
            }
        }

    }
}