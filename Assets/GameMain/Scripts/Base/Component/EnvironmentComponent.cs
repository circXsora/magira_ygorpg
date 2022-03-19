//------------------------------------------------------------------------------
//  <copyright file="EnvironmentComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:56:48
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
    public enum EnvironmentType
    {
        Environment_1,
    }

    public class EnvironmentComponent : GameFrameworkComponent
    {
        [SerializeField]
        private Transform parent;
        [SerializeField]
        private EventSO LoadEnvironmentComplete;
        private Dictionary<string, GameObject> environmentDic = new Dictionary<string, GameObject>();
		public async Task<EnvironmentEntity> Load(EnvironmentType environment)
        {
            var env = await GameEntry.Resource.LoadAsync<GameObject>("Environments/" + environment.ToString());
            var instance = Instantiate(env, parent);
            var entity = instance.GetOrAddComponent<EnvironmentEntity>();
            entity.Type = environment;
            environmentDic.Add(environment.ToString(), instance);
            LoadEnvironmentComplete?.Raise(this, instance);
            return entity;
        }

        public EnvironmentBindings[] GetEnvironmentContext(EnvironmentType environmentType)
        {
            return environmentDic[environmentType.ToString()].GetComponentsInChildren<EnvironmentBindings>();
        }

        public async Task Release(EnvironmentType environment)
        {
            await Task.Run(() =>
            {
                if (environmentDic.TryGetValue(environment.ToString(), out var env))
                {
                    environmentDic.Remove(environment.ToString());
                    Destroy(env);
                }
            }
            );
        }
    }
}