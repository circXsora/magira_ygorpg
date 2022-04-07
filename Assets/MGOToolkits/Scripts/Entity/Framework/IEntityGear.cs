//------------------------------------------------------------------------------
//  <copyright file="IEntityComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/14 17:07:16
//  项目:  MGO
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO.Entity
{
	public interface IEntityGear
	{
		IEntity GetOwner();
	}
}