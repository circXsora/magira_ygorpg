//------------------------------------------------------------------------------
//  <copyright file="TragetTracker.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/24 12:13:01
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class TragetTracker : MonoBehaviour
	{
        public Transform Target { get; set; }
        void Start()
		{
	
		}
	
		void Update()
		{
            if (Target != null)
            {
				transform.position = Target.position;
            }
		}
	}
}