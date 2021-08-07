//------------------------------------------------------------------------------
//  <copyright file="SoraResourceComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 11:53:20
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
	public class SoraResourceComponent : UnityGameFramework.Runtime.GameFrameworkComponent
	{
		public T Load<T>(string path) where T : Object
        {
			return Resources.Load<T>(path); 
        }

		public async Task<T> LoadAsync<T>(string path) where T : Object
		{
			var loadRequest = Resources.LoadAsync<T>(path);
            while (!loadRequest.isDone)
            {
				await Task.Yield();
            }
			return (T) loadRequest.asset;
		}

		public Task<Texture> LoadBg(SoraUIForm.BgType bgType)
        {
			return LoadAsync<Texture>("UI/Background/" + bgType.ToString());
        }
	}
}