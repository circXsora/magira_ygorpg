//------------------------------------------------------------------------------
//  <copyright file="ProcedureManager.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:23:07
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class ProcedureManager : IProcedureManager
    {
        public ProcedureBaseSO CurrentProcedure => throw new System.NotImplementedException();

        public float CurrentProcedureTime => throw new System.NotImplementedException();

        public ProcedureBaseSO GetProcedure(string procedureName)
        {
            throw new System.NotImplementedException();
        }

        public bool HasProcedure(ProcedureBaseSO procedureType)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(params ProcedureBaseSO[] procedures)
        {
            throw new System.NotImplementedException();
        }

        public void StartProcedure(ProcedureBaseSO procedureType)
        {
            throw new System.NotImplementedException();
        }
    }
}