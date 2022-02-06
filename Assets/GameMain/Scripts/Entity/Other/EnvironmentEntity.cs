//------------------------------------------------------------------------------
//  <copyright file="EnvironmentEntity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/1/2 15:48:09
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
	public class EnvironmentEntity : Entity
	{
        public EnvironmentType Type;
        private EnvironmentBindings[] contexts;
		public EnvironmentBindings GetContext(int index = 0)
        {
            if (contexts== null)
            {
				contexts = GetComponentsInChildren<EnvironmentBindings>();
            }
			return contexts[index];
		}
    }
}