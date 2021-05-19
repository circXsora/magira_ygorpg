//------------------------------------------------------------------------------
//  <copyright file="UIControlData.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/19 21:30:49
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public class UIControlData : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _uiControls = new List<GameObject>();

        private IDictionary<string, GameObject> _uiControlsDictionary;

        private void Awake()
        {
            _uiControlsDictionary = new Dictionary<string, GameObject>();
            foreach (var uiContorl in _uiControls)
            {
                if (!_uiControlsDictionary.ContainsKey(uiContorl.name))
                {
                    _uiControlsDictionary.Add(uiContorl.name, uiContorl);
                }
                else
                {
                    Log.Warning("UIContolData里添加了多个相同的元素");
                }
            }
            _uiControls = null;
        }

        public T Get<T>(string name) where T : Component
        {
            if (_uiControlsDictionary.TryGetValue(name, out var obj))
            {
                return obj.GetComponent<T>();
            }
            return null;
        }
    }
}