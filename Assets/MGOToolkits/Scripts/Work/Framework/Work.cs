//------------------------------------------------------------------------------
//  <copyright file="Work.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/26 21:54:10
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;

namespace MGO
{
    /// <summary>
    /// 工作的生命周期 <see cref="Work"/>.
    /// </summary>
    public enum WorkStatus
    {
        /// <summary> 
        /// 工作被创建出来
        /// </summary>
        Created,
        /// <summary>
        /// 工作正在执行
        /// </summary>
        Running,
        /// <summary>
        /// 工作完成
        /// </summary>
        Complete,
        /// <summary>
        /// 工作被停止了
        /// </summary>
        Stop,
        /// <summary>
        /// 工作出错了
        /// </summary>
        Faulted
    }

    public abstract class Work : IReference
    {

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name == null)
                {
                    _name = value;
                }
                else
                {
                    throw new InvalidOperationException($"{Name} Work 已经被赋值，不能被重复赋值。");
                }
            }
        }

        public object UserData { get; private set; }

        public WorkStatus WorkStatus { get; private set; } = WorkStatus.Created;

        private Action<Work> _onComplete;

        private Action<Work> _onStart;

        private Action<Work> _onStop;

        protected abstract bool IsComplete { get; }

        public void Start()
        {
            if (WorkStatus == WorkStatus.Created || WorkStatus == WorkStatus.Stop)
            {
                Log.Debug("======Work======" + Name + " Start ");

                WorkStatus = WorkStatus.Running;
                StartCore();
                _onStart?.Invoke(this);
            }
            else
            {
                throw new InvalidOperationException($"在 {Name} Work中，工作是{WorkStatus}状态，却又开始执行了。");
            }
        }

        protected virtual void StartCore() { }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (WorkStatus == WorkStatus.Running)
            {
                UpdateCore(elapseSeconds, realElapseSeconds);

                if (IsComplete)
                {
                    Log.Debug("======Work======" + Name + " Complete ");
                    WorkStatus = WorkStatus.Complete;
                    _onComplete?.Invoke(this);
                }
            }
            else
            {
                throw new InvalidOperationException($"在 {Name} Work中，工作是{WorkStatus}状态，却正在运行。");
            }

        }

        protected virtual void UpdateCore(float elapseSeconds, float realElapseSeconds) { }

        public void Stop()
        {
            if (WorkStatus == WorkStatus.Running)
            {
                Log.Debug("======Work======" + Name + " Stop ");
                WorkStatus = WorkStatus.Stop;
                StopCore();
                _onStop?.Invoke(this);
            }
            else
            {
                throw new InvalidOperationException($"在 {Name} Work中，工作是{WorkStatus}状态，却被停止掉了。");
            }
        }

        protected virtual void StopCore() { }

        /// <summary>
        /// 工作完成后的委托
        /// </summary>
        /// <param name="afterCompleteDelegate"></param>
        /// <returns></returns>
        public Work ContinueWith(Action<Work> afterCompleteDelegate)
        {
            _onComplete += afterCompleteDelegate;
            return this;
        }

        /// <summary>
        /// 工作开始前的委托
        /// </summary>
        /// <param name="beforeWorkDelegate"></param>
        /// <returns></returns>
        public Work BeginWith(Action<Work> beforeWorkDelegate)
        {
            _onStart += beforeWorkDelegate;
            return this;
        }

        /// <summary>
        /// 工作停止后的委托
        /// </summary>
        /// <param name="afterStopDelegate"></param>
        /// <returns></returns>
        public Work StopWith(Action<Work> afterStopDelegate)
        {
            _onStop += afterStopDelegate;
            return this;
        }

        /// <summary>
        /// 填充用户数据
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public Work FillData(object userData)
        {
            if (UserData == null)
            {
                UserData = userData;
            }
            else
            {
                WorkStatus = WorkStatus.Faulted;
                throw new System.InvalidOperationException($"在 {Name} Work中已经填充过用户数据了，请勿重复填充。");
            }
            return this;
        }

        public virtual void Clear()
        {
            Log.Debug("======Work======" + Name + " Clear ");
            _name = null;
            UserData = null;
            _onComplete = null;
            _onStart = null;
            _onStop = null;
            WorkStatus = WorkStatus.Created;
        }
    }
}