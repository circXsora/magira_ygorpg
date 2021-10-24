using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace BBYGO
{
    public class TimePointHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress">进度（0-1）</param>
        /// <param name="totalTime">总时长</param>
        /// <param name="timePoint"></param>
        public delegate void ProgressHanlder(float progress, float totalTime, VisualEffectParam visualEffect);
        /// <summary>
        /// 前摇开始
        /// </summary>
        public ProgressHanlder start;
        /// <summary>
        /// 真实触发点
        /// </summary>
        public ProgressHanlder realEffect;
        /// <summary>
        /// 全部完成点
        /// </summary>
        public ProgressHanlder end;
    }

    public class VisualEffectComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {

        public void SetVisualEffectParam(CreatureView creatureView, VisualEffectParam visualEffectParam)
        {

        }

        public async Task Perform(CreatureView creatureView, VisualEffectParam visualEffectParam, TimePointHandler[] timePointCallbacks = null)
        {
            var task = Task.Delay(visualEffectParam.totalTime.ToMillisecond());
            var totalTime = visualEffectParam.totalTime;
            var startTimePointTasks = new List<Task>();
            var realEffectTimePointTasks = new List<Task>();
            var endTimePointTasks = new List<Task>();
            var timePoints = visualEffectParam.timePoints;
            if (timePoints != null)
            {
                for (int i = 0; i < timePoints.Count; i++)
                {
                    var timePoint = timePoints[i];
                    startTimePointTasks.Add(Task.Delay((timePoint.startEffectTimePoint * totalTime).ToMillisecond()));
                    realEffectTimePointTasks.Add(Task.Delay((timePoint.realEffectTimePoint * totalTime).ToMillisecond()));
                    endTimePointTasks.Add(Task.Delay((timePoint.endEffectTimePoint * totalTime).ToMillisecond()));
                }
            }

            if (timePointCallbacks != null)
            {
                for (int i = 0; i < startTimePointTasks.Count; i++)
                {
                    await startTimePointTasks[i];
                    timePointCallbacks[i].start?.Invoke(timePoints[i].startEffectTimePoint, totalTime, visualEffectParam);
                    await realEffectTimePointTasks[i];
                    timePointCallbacks[i].realEffect?.Invoke(timePoints[i].realEffectTimePoint, totalTime, visualEffectParam);
                    await endTimePointTasks[i];
                    timePointCallbacks[i].end?.Invoke(timePoints[i].endEffectTimePoint, totalTime, visualEffectParam);
                }
            }

            await task;
        }
    }

}