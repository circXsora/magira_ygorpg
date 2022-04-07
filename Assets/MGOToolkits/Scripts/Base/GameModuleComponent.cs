//------------------------------------------------------------------------------
//  <copyright file="GameModuleComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/20 21:10:26
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
    public class GameModuleComponent : GameComponent
    {
        private List<GameModule> components = new List<GameModule>();
        public T GetInstance<T>() where T : GameModule
        {
            var instance = Activator.CreateInstance<T>();
            components.Add(instance);
            return instance;
        }

        private void Update()
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                var component = components[i];
                if (component == null || component.IsShutDown)
                {
                    components.RemoveAt(i);
                }
                component.Update(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        private void OnDestroy()
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                var component = components[i];
                if (component != null)
                {
                    component.Shutdown();
                }
            }
            components.Clear();
        }
    }
}