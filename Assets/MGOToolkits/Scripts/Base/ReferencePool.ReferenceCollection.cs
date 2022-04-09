//------------------------------------------------------------------------------
//  <copyright file="ReferencePool.ReferenceCollection.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/17 22:49:40
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace MGO
{
    public static partial class ReferencePool
    {
        private sealed class ReferenceCollection
        {
            private readonly Queue<IReference> m_References;
            private readonly Type m_ReferenceType;
            private int m_UsingReferenceCount;
            private int m_AcquireReferenceCount;
            private int m_ReleaseReferenceCount;
            private int m_AddReferenceCount;
            private int m_RemoveReferenceCount;

            public ReferenceCollection(Type referenceType)
            {
                m_References = new Queue<IReference>();
                m_ReferenceType = referenceType;
                m_UsingReferenceCount = 0;
                m_AcquireReferenceCount = 0;
                m_ReleaseReferenceCount = 0;
                m_AddReferenceCount = 0;
                m_RemoveReferenceCount = 0;
            }

            public Type ReferenceType
            {
                get
                {
                    return m_ReferenceType;
                }
            }

            public int UnusedReferenceCount
            {
                get
                {
                    return m_References.Count;
                }
            }

            public int UsingReferenceCount
            {
                get
                {
                    return m_UsingReferenceCount;
                }
            }

            public int AcquireReferenceCount
            {
                get
                {
                    return m_AcquireReferenceCount;
                }
            }

            public int ReleaseReferenceCount
            {
                get
                {
                    return m_ReleaseReferenceCount;
                }
            }

            public int AddReferenceCount
            {
                get
                {
                    return m_AddReferenceCount;
                }
            }

            public int RemoveReferenceCount
            {
                get
                {
                    return m_RemoveReferenceCount;
                }
            }

            public T Acquire<T>() where T : class, IReference, new()
            {
                if (typeof(T) != m_ReferenceType)
                {
                    throw new System.ArgumentException("Type is invalid.");
                }

                m_UsingReferenceCount++;
                m_AcquireReferenceCount++;
                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        return (T)m_References.Dequeue();
                    }
                }

                m_AddReferenceCount++;
                return new T();
            }

            public T AcquireWithoutSpawn<T>() where T : class, IReference
            {
                if (typeof(T) != m_ReferenceType)
                {
                    throw new System.ArgumentException("Type is invalid.");
                }


                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        m_UsingReferenceCount++;
                        m_AcquireReferenceCount++;
                        return (T)m_References.Dequeue();
                    }
                }

                return null;
            }


            public IReference Acquire()
            {
                m_UsingReferenceCount++;
                m_AcquireReferenceCount++;
                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        return m_References.Dequeue();
                    }
                }

                m_AddReferenceCount++;
                return (IReference)Activator.CreateInstance(m_ReferenceType);
            }

            public IReference AcquireWithoutSpawn()
            {

                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        m_UsingReferenceCount++;
                        m_AcquireReferenceCount++;
                        return m_References.Dequeue();
                    }
                }

                return null;
            }

            public void Release(IReference reference)
            {
                reference.Clear();
                lock (m_References)
                {
                    if (m_EnableStrictCheck && m_References.Contains(reference))
                    {
                        throw new System.InvalidOperationException("The reference has been released.");
                    }

                    m_References.Enqueue(reference);
                }

                m_ReleaseReferenceCount++;
                m_UsingReferenceCount--;
                if (m_UsingReferenceCount < 0)
                {
                    m_UsingReferenceCount = 0;
                }
            }

            public void Add<T>(int count) where T : class, IReference, new()
            {
                if (typeof(T) != m_ReferenceType)
                {
                    throw new System.ArgumentException("Type is invalid.");
                }

                lock (m_References)
                {
                    m_AddReferenceCount += count;
                    while (count-- > 0)
                    {
                        m_References.Enqueue(new T());
                    }
                }
            }

            public void Add(int count)
            {
                lock (m_References)
                {
                    m_AddReferenceCount += count;
                    while (count-- > 0)
                    {
                        m_References.Enqueue((IReference)Activator.CreateInstance(m_ReferenceType));
                    }
                }
            }

            public void Remove(int count)
            {
                lock (m_References)
                {
                    if (count > m_References.Count)
                    {
                        count = m_References.Count;
                    }

                    m_RemoveReferenceCount += count;
                    while (count-- > 0)
                    {
                        m_References.Dequeue();
                    }
                }
            }

            public void RemoveAll()
            {
                lock (m_References)
                {
                    m_RemoveReferenceCount += m_References.Count;
                    m_References.Clear();
                }
            }
        }
    }
}
