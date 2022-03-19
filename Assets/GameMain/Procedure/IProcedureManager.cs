//------------------------------------------------------------------------------
//  <copyright file="IProcedureManager.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:12:49
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using UnityEngine;

namespace BBYGO
{
	public interface IProcedureManager
	{
        /// <summary>
        /// 获取当前流程。
        /// </summary>
        ProcedureBaseSO CurrentProcedure
        {
            get;
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        float CurrentProcedureTime
        {
            get;
        }

        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">有限状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        void Initialize(params ProcedureBaseSO[] procedures);

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <param name="procedureType">要开始的流程。</param>
        void StartProcedure(ProcedureBaseSO procedureType);

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <returns>是否存在流程。</returns>
        bool HasProcedure(ProcedureBaseSO procedureType);

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <returns>要获取的流程。</returns>
        ProcedureBaseSO GetProcedure(string procedureName);
    }
}