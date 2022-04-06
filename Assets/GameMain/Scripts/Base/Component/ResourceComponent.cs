//------------------------------------------------------------------------------
//  <copyright file="SoraResourceComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 11:53:20
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class ResourceComponent : GameComponent
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async Task<T> LoadAsync<T>(string path) where T : Object
        {
            try
            {
                var loadRequest = Resources.LoadAsync<T>(path);
                if (loadRequest == null)
                {
                    throw new System.InvalidOperationException($"{path} 路径无效，对象不存在");
                }

                float startLoadTime = Time.realtimeSinceStartup;
                while (!loadRequest.isDone)
                {
                    if (Time.realtimeSinceStartup - startLoadTime > 2f)
                    {
                        throw new System.InvalidOperationException("加载超时:" + path);
                    }
                    await Task.Yield();
                }
                if (loadRequest.asset == null)
                {
                    throw new System.InvalidOperationException($"{path} 路径无效，对象不存在");
                }
                return (T)loadRequest.asset;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<Texture> LoadBg(BgType bgType)
        {
            return LoadAsync<Texture>("UI/Background/" + bgType.ToString());
        }
    }
}