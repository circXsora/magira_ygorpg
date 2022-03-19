using System;

namespace MGO
{
    public class WaitWork : Work
    {
        protected WaitWork() { }
        private Func<WaitWork, bool> _completeCondition;
        protected override bool IsComplete => _completeCondition(this);

        public static WaitWork Create(Func<WaitWork, bool> completeCondition)
        {
            var work = ReferencePool.AcquireWithoutSpawn<WaitWork>() ?? new WaitWork();
            work._completeCondition = completeCondition ?? throw new ArgumentNullException("�ڵȴ������У������������ֵΪ�ա�");
            return work;
        }

        public override void Clear()
        {
            base.Clear();
            _completeCondition = default;
        }
    }

}