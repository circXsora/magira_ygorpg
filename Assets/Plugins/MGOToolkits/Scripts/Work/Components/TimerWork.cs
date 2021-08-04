//------------------------------------------------------------------------------
//  <copyright file="TimerWork.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/29 9:18:08
//  项目:  MGOToolkits
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public class TimerWork : Work
    {
        public float Interval { get; set; }
        public bool AlwaysRun { get; private set; }
        public int TotalLoopTimes { get; private set; }
        public int LeftLoopTimes { get; private set; }
        private float _currentInterval;
        private Action _workDelegate;
        private Func<TimerWork, bool> _endCondition;

        protected TimerWork() { }

        public static TimerWork Create(float interval, Action workDelegate)
        {
            return Create(interval, workDelegate, null, 0);
        }

        public static TimerWork Create(float interval, Action workDelegate, Func<TimerWork, bool> endCondition)
        {
            return Create(interval, workDelegate, endCondition, 0);
        }

        public static TimerWork Create(float interval, Action workDelegate, int loopTimes)
        {
            return Create(interval, workDelegate, null, loopTimes);
        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        /// <param name="interval">定时器间隔</param>
        /// <param name="workDelegate">定时器工作委托</param>
        /// <param name="endCondition">如果这个条件达成，那么定时器也会完成</param>
        /// <param name="loopTimes">如果传入0或者0以下的值，那么定时器会一直执行下去</param>
        /// <returns></returns>
        public static TimerWork Create(float interval, Action workDelegate, Func<TimerWork, bool> endCondition, int loopTimes)
        {
            var work = GameFramework.ReferencePool.AcquireWithoutSpawn<TimerWork>() ?? new TimerWork();
            if (interval <= 0)
            {
                throw new ArgumentException($"创建{nameof(TimerWork)}时，时间间隔参数{nameof(interval)}不正确，这个值应该大于0，但传入的值为{interval}。");
            }
            if (workDelegate == null)
            {
                throw new ArgumentNullException($"创建{nameof(TimerWork)}时，工作委托参数{nameof(workDelegate)}传入的值空，考虑使用{nameof(WaitWork)}或者{nameof(WaitSecondsWork)}。");
            }
            work._workDelegate = workDelegate;
            work._endCondition = endCondition;
            work.Interval = interval;
            if (loopTimes > 0)
            {
                work.AlwaysRun = false;
                work.TotalLoopTimes = loopTimes;
                work.LeftLoopTimes = loopTimes;
            }
            else
            {
                work.AlwaysRun = true;
                work.TotalLoopTimes = 0;
                work.LeftLoopTimes = 0;
            }
            work._currentInterval = interval;
            return work;
        }

        protected override bool IsComplete
        {
            get
            {
                if (_endCondition != null)
                {
                    return _endCondition(this);
                }
                if (!AlwaysRun)
                {
                    return LeftLoopTimes <= 0;
                }
                return false;
            }
        }

        protected override void UpdateCore(float elapseSeconds, float realElapseSeconds)
        {
            if (_currentInterval > elapseSeconds)
            {
                _currentInterval -= elapseSeconds;
            }
            else
            {
                while (elapseSeconds >= _currentInterval)
                {
                    _workDelegate?.Invoke();
                    elapseSeconds -= _currentInterval;
                    _currentInterval = Interval;
                    if (!AlwaysRun)
                    {
                        LeftLoopTimes--;
                        if (LeftLoopTimes <= 0)
                        {
                            return;
                        }
                    }
                }
            }
        }

        public override void Clear()
        {
            base.Clear();
            Interval = default;
            AlwaysRun = default;
            TotalLoopTimes = default;
            LeftLoopTimes = default;
            _currentInterval = default;
            _workDelegate = default;
            _endCondition = default;
        }
    }
}