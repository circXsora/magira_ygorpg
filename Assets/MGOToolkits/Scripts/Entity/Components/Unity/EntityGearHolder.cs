//------------------------------------------------------------------------------
//  <copyright file="EntityComponentHolder.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/14 17:29:10
//  项目:  MGO
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGO.Entity.Unity
{
    /// <summary>
    /// 实体组件管理器，用于管理Entity上的零件
    /// </summary>
    public class EntityGearHolder : MonoBehaviour
    {
        private List<EntityGear> components = new List<EntityGear>();
        public Entity Owner { get; set; }

        public IEnumerable<EntityGear> GetAll()
        {
            return components;
        }

        public T Get<T>() where T : EntityGear
        {
            return (T)components.First(c => c is T);
        }

        public T Add<T>() where T : EntityGear
        {
            var comp = Owner.gameObject.AddComponent<T>();
            components.Add(comp);
            comp.entity = Owner;
            comp.OnAdd();
            return comp;
        }

        public void Remove(EntityGear component)
        {
            components.Remove(component);
            component.OnRemove();
            Destroy(component);
        }

        public T RemoveAt<T>(int index) where T : EntityGear
        {
            var comp = components[index];
            components.RemoveAt(index);
            return (T)comp;
        }

        public IEntity GetOwner()
        {
            return Owner;
        }
    }
}