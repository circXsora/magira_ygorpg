//------------------------------------------------------------------------------
//  <copyright file="WorkManager.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/26 22:09:20
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections.Generic;

namespace MGO
{
    public class WorkManager
    {

        public bool RecycleOnStop { get; set; }
        public bool RecycleOnComplete { get; set; }

        private readonly LinkedList<Work> _workNodes = new LinkedList<Work>();
        private readonly LinkedList<Work> _tempDeleteWorkNodes = new LinkedList<Work>();

        public virtual void StartWork(Work work)
        {
            work.Start();
            _workNodes.AddLast(work);
        }
        public virtual void StopWork(Work work)
        {
            work.Stop();
            _workNodes.Remove(work);
        }
        /// <summary>
        /// 当不再需要使用Work时，释放它以避免GC，方便下次使用
        /// </summary>
        /// <param name="work"></param>
        public virtual void ReleaseWork(Work work)
        {
            GameFramework.ReferencePool.Release(work);
        }
        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            var node = _workNodes.First;
            while (node != null)
            {
                var work = node.Value;
                if (work != null)
                {
                    if (work.WorkStatus == WorkStatus.Running)
                    {
                        work.Update(elapseSeconds, realElapseSeconds);
                    }
                    if (work.WorkStatus == WorkStatus.Complete)
                    {
                        _tempDeleteWorkNodes.AddLast(work);
                        if (RecycleOnComplete)
                        {
                            ReleaseWork(work);
                        }
                    }
                    else if (work.WorkStatus == WorkStatus.Stop)
                    {
                        _tempDeleteWorkNodes.AddLast(work);
                        if (RecycleOnStop)
                        {
                            ReleaseWork(work);
                        }
                    }
                }
                node = node.Next;
            }

            foreach (var work in _tempDeleteWorkNodes)
            {
                _workNodes.Remove(work);
            }

            if (_tempDeleteWorkNodes.Count > 0)
            {
                _tempDeleteWorkNodes.Clear();
            }
        }
    }
}