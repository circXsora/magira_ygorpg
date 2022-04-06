//------------------------------------------------------------------------------
//  <copyright file="ProcedureManager.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:23:07
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
    public class ProcedureManager : GameModule, IProcedureManager
    {
        public ProcedureBase CurrentProcedure { get; set; }

        public float CurrentProcedureTime { get; set; }

        public ProcedureBase[] Procedures { get; set; }

        public ProcedureBase GetProcedure(string procedureName)
        {
            return Array.Find(Procedures, x => x.ProcedureName == procedureName);
        }

        public ProcedureBase GetProcedure(ProcedureBase procedure)
        {
            return Array.Find(Procedures, x => x == procedure);
        }

        public bool HasProcedure(ProcedureBase procedure)
        {
            return Array.Find(Procedures, x => x == procedure) != null;
        }

        public void Initialize(params ProcedureBase[] procedures)
        {
            Procedures = procedures;
            for (int i = 0; i < Procedures.Length; i++)
            {
                Procedures[i].OnInit();
            }
        }

        public void StartProcedure(ProcedureBase procedure)
        {
            CurrentProcedure?.OnLeave(false);
            CurrentProcedure = procedure;
            CurrentProcedure.OnEnter();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            CurrentProcedure?.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}