//------------------------------------------------------------------------------
//  <copyright file="ParallelWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/29 21:38:43
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MGO
{
    public class ParallelWork : Work
    {
        public List<Work> RunningWorks { get; private set; } = new List<Work>();
        public List<Work> CompleteWorks { get; private set; } = new List<Work>();

        protected ParallelWork() { }

        public static ParallelWork Create(params Work[] works)
        {
            var work = ReferencePool.AcquireWithoutSpawn<ParallelWork>() ?? new ParallelWork();
            work.RunningWorks.AddRange(works);
            return work;
        }

        protected override bool IsComplete => RunningWorks.Count == 0;

        protected override void StartCore()
        {
            base.StartCore();
            foreach (var work in RunningWorks)
            {
                work.Start();
            }
        }

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            for (int i = RunningWorks.Count - 1; i >= 0; i--)
            {
                var work = RunningWorks[i];
                if (work.WorkStatus == WorkStatus.Running)
                {
                    work.Update(elapseSeconds, realElapseSeconds);
                }
                if (work.WorkStatus == WorkStatus.Complete)
                {
                    CompleteWorks.Add(work);
                    RunningWorks.RemoveAt(i);
                }
            }
        }

        protected override void StopCore()
        {
            base.StopCore();
            foreach (var work in RunningWorks)
            {
                work.Stop();
            }
        }

        public override void Clear()
        {
            base.Clear();
            foreach (var work in RunningWorks)
            {
                ReferencePool.Release(work);
            }
            foreach (var work in CompleteWorks)
            {
                ReferencePool.Release(work);
            }
            RunningWorks.Clear();
            CompleteWorks.Clear();
        }
    }
}