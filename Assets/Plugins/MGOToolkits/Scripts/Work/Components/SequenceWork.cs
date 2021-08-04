//------------------------------------------------------------------------------
//  <copyright file="SequenceWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/29 16:57:38
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MGO
{
    public class SequenceWork : Work
    {
        public List<Work> Works { get; private set; } = new List<Work>();
        public Work CurrentWork => Works[CurrentWorkIndex];
        public int CurrentWorkIndex { get; private set; } = 0;

        protected SequenceWork() { }

        public static SequenceWork Create(params Work[] works)
        {
            var work = GameFramework.ReferencePool.AcquireWithoutSpawn<SequenceWork>() ?? new SequenceWork();
            if (works != null)
            {
                work.Works.AddRange(works);
            }
            return work;
        }

        protected override bool IsComplete => CurrentWorkIndex == Works.Count - 1 && CurrentWork.WorkStatus == WorkStatus.Complete;

        protected override void StartCore()
        {
            base.StartCore();
            CurrentWork.Start();
        }

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            if (CurrentWork.WorkStatus == WorkStatus.Running)
            {
                CurrentWork.Update(elapseSeconds, realElapseSeconds);
            }
            if (CurrentWork.WorkStatus == WorkStatus.Complete)
            {
                if (CurrentWorkIndex + 1 < Works.Count)
                {
                    CurrentWorkIndex++;
                    CurrentWork.Start();
                }
            }
        }

        protected override void StopCore()
        {
            base.StopCore();
            for (int i = CurrentWorkIndex; i < Works.Count; i++)
            {
                Works[i].Stop();
            }
        }

        public override void Clear()
        {
            base.Clear();
            foreach (var work in Works)
            {
                GameFramework.ReferencePool.Release(work);
            }
            Works.Clear();
            CurrentWorkIndex = 0;
        }
    }
}