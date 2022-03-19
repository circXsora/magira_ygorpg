namespace MGO
{
    public class WaitOneFrameWork : Work
    {
        protected WaitOneFrameWork() { }
        private bool _isComplete = false;
        protected override bool IsComplete => _isComplete;

        public static WaitOneFrameWork Create(float waitSeconds)
        {
            var work = ReferencePool.AcquireWithoutSpawn<WaitOneFrameWork>() ?? new WaitOneFrameWork();
            return work;
        }

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            _isComplete = true;
        }

        public override void Clear()
        {
            base.Clear();
            _isComplete = false;
        }
    }

}