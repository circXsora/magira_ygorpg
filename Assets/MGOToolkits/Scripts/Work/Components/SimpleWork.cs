//------------------------------------------------------------------------------
//  <copyright file="SimpleWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/26 22:02:59
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;

namespace MGO
{
    public class SimpleWork : Work
    {
        private Action<SimpleWork> _workDelegate;
        private bool _isComplete;

        protected SimpleWork() { }

        public static SimpleWork Create(Action<SimpleWork> workDelegate)
        {
            var work = ReferencePool.AcquireWithoutSpawn<SimpleWork>() ?? new SimpleWork();
            work._isComplete = false;
            work._workDelegate = workDelegate;
            return work;
        }

        public static SimpleWork Create(Action workDelegate)
        {
            var work = ReferencePool.AcquireWithoutSpawn<SimpleWork>() ?? new SimpleWork();
            work._isComplete = false;
            work._workDelegate = (_) => workDelegate?.Invoke();
            return work;
        }

        protected override bool IsComplete => _isComplete;

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            _workDelegate?.Invoke(this);
            _isComplete = true;
        }

        public override void Clear()
        {
            base.Clear();
            _workDelegate = null;
            _isComplete = false;
        }
    }
}