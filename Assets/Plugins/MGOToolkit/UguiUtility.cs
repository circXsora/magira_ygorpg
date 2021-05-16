//------------------------------------------------------------------------------
//  <copyright file="UguiUtility.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/16 21:16:09
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
	public static class UguiUtility 
	{
		public static Func<GameObject> CreateCellGenerator(GameObject cell, Transform cellParent)
        {
            for (int i = 0; i < cellParent.childCount; i++)
            {
                cellParent.GetChild(i).gameObject.SetActive(false);
            }

            int cellIndex = 0;
            GameObject nextCell = null;
            return GetNextCell;
            GameObject GetNextCell()
            {
                if (cellParent.childCount < cellIndex)
                {
                    nextCell = GameObject.Instantiate(cell, cellParent);
                }
                else
                {
                    nextCell = cellParent.GetChild(cellIndex).gameObject;
                }

                return nextCell;
            }
        }
	}
}