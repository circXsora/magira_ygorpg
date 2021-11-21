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
    public class EntityComponentHolder : MonoBehaviour
    {
        private List<EntityComponent> components = new List<EntityComponent>();
        public Entity Owner { get; set; }

        public IEnumerable<EntityComponent> GetAll()
        {
            return components;
        }

        public T Get<T>() where T : EntityComponent
        {
            return (T)components.First(c => c.GetType() == typeof(T));
        }

        public T Add<T>() where T : EntityComponent
        {
            var comp = Owner.gameObject.AddComponent<T>();
            components.Add(comp);
            comp.entity = Owner;
            return comp;
        }

        public void Remove(EntityComponent component)
        {
            components.Remove(component);
            Destroy(component);
        }

        public T RemoveAt<T>(int index) where T : EntityComponent  
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