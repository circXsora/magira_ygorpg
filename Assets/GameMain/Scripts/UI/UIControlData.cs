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

        private IDictionary<string, int> _name4Index = new Dictionary<string, int>();

        private void Awake()
        {
            _name4Index = new Dictionary<string, int>();
            for (int i = 0; i < _uiControls.Count; i++)
            {
                var uiContorl = _uiControls[i];
                if (uiContorl != null)
                {
                    if (!_name4Index.ContainsKey(uiContorl.name))
                    {
                        _name4Index.Add(uiContorl.name, i);
                    }
                }
            }
        }

        public T Get<T>(string name) where T : Component
        {
            if (_name4Index.TryGetValue(name, out var index))
            {
                return _uiControls[index].GetComponent<T>();
            }
            return null;
        }
    }
}