namespace MGO
{
    public class WaitSecondsWork : Work
    {
        public float WaitSeconds { get; private set; }
        public float LeftSeconds { get; private set; }

        protected WaitSecondsWork() { }

        protected override bool IsComplete => LeftSeconds <= 0;

        public static WaitSecondsWork Create(float waitSeconds)
        {
            var work = ReferencePool.AcquireWithoutSpawn<WaitSecondsWork>() ?? new WaitSecondsWork();
            work.WaitSeconds = waitSeconds;
            work.LeftSeconds = waitSeconds;
            return work;
        }

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            LeftSeconds -= elapseSeconds;
        }

        public override void Clear()
        {
            base.Clear();
            WaitSeconds = default;
            LeftSeconds = default;
        }
    }

}