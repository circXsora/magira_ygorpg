using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace BBYGO
{
    public class ProcedureEventArgs
    {

    }

    /// <summary>
    /// 用于管理事件
    /// </summary>
    public class EventComponent : GameComponent
    {
        public IEventManager<ProcedureEventArgs> ProcedureEnd = new UniversalEventManager<ProcedureEventArgs>();
    }


    public class Listener<T>
    {
        public int priority;
        public EventHandler<T> handler;

        public IEventManager<T> manager;

        ~Listener()
        {
            manager?.RemoveListener(this);
        }

    }

    public class ListenerComparer<T> : IComparer<Listener<T>>
    {
        public int Compare(Listener<T> x, Listener<T> y)
        {
            return x.priority.CompareTo(y.priority);
        }
    }

    public interface IEventManager<T>
    {
        public void AddListener(Listener<T> listener);
        public void RemoveListener(Listener<T> listener);

        public void Raise(object sender, T arg);
    }

    public class UniversalEventManager<T> : IEventManager<T>
    {
        public string name;
        public SortedSet<Listener<T>> events = new SortedSet<Listener<T>>(new ListenerComparer<T>());

        public void AddListener(Listener<T> listener)
        {
            if (!events.Add(listener))
            {
                Log.Error("加入监听出错{0}", nameof(name));
            }
        }

        public void RemoveListener(Listener<T> listener)
        {
            if (!events.Remove(listener))
            {
                Log.Error("移除监听出错{0}", nameof(name));
            }
        }

        public void Raise(object sender, T arg)
        {
            foreach (var e in events)
            {
                e.handler?.Invoke(sender, arg);
            }
        }
    }
}