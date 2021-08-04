using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MGO.Entity.Unity
{
    public class EntityViewBinder : MonoBehaviour, IEntityViewBinder
    {
        [SerializeField] private List<Component> _components = new List<Component>();

        private void Awake()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _name4Index.Add(_components[i].name, i);
            }
        }

        private Dictionary<string, int> _name4Index = new Dictionary<string, int>();

        public TViewControl Get<TViewControl>(string name) where TViewControl : class
        {
            if (_name4Index.TryGetValue(name, out int index))
            {
                return _components[index] as TViewControl;
            }
            else
            {
                throw new System.ArgumentException($"在{gameObject.name}物体上的视图绑定器没有绑定过名字为{name}的控件。");
            }
        }
    }
}