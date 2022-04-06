//------------------------------------------------------------------------------
//  <copyright file="ProcudureComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:10:35
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    /// <summary>
    /// 流程组件。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ProcedureComponent : GameComponent
    {
        private IProcedureManager m_ProcedureManager = null;
        private ProcedureBase m_EntranceProcedure = null;
        private Listener<ProcedureEventArgs> procedureEndListener = null;

        private ProcedureBase[] m_ProcedureQueue = null;

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                return m_ProcedureManager.CurrentProcedure;
            }
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                return m_ProcedureManager.CurrentProcedureTime;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            m_ProcedureQueue = GetComponentsInChildren<ProcedureBase>();
            m_EntranceProcedure = m_ProcedureQueue[0];
            procedureEndListener = new Listener<ProcedureEventArgs>() { priority = 0, handler = OnProcedureEnd };
            GameEntry.Event.ProcedureEnd.AddListener(procedureEndListener);

            m_ProcedureManager = GameEntry.GameModule.GetInstance<ProcedureManager>();
            if (m_ProcedureManager == null)
            {
                Log.Fatal("Procedure manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            m_ProcedureManager.Initialize(m_ProcedureQueue);
            m_ProcedureManager.StartProcedure(m_EntranceProcedure);
        }

        private void OnDestroy()
        {
            GameEntry.Event.ProcedureEnd.RemoveListener(procedureEndListener);
        }

        public void OnProcedureEnd(object sender, ProcedureEventArgs arg)
        {
            var index = Array.IndexOf(m_ProcedureQueue, CurrentProcedure);
            index++;
            m_ProcedureManager.StartProcedure(m_ProcedureQueue[index]);
        }


        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <returns>是否存在流程。</returns>
        //public bool HasProcedure(string procedureName)
        //{
        //    return m_ProcedureManager.HasProcedure(procedureName);
        //}

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(ProcedureBase procedure)
        {
            return m_ProcedureManager.HasProcedure(procedure);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure(string procedureName)
        {
            return m_ProcedureManager.GetProcedure(procedureName);
        }
    }
}