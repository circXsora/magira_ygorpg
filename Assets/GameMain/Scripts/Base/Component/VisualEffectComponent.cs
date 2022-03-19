using DG.Tweening;
using MGO;
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

    public class VisualEffectComponent : GameFrameworkComponent
    {
        public GameObject TextEffect1;

        private GameObject GetTextEffect(VisualEffectTypeSO type)
        {
            return TextEffect1;
        }

        public async Task PerformNumberTextEffect(Vector3 position, int nubmer, VisualEffectTypeSO type)
        {
            var textEffect = Instantiate(GetTextEffect(type), transform).GetComponent<NumberTextEffect>();
            textEffect.transform.position = position;
            textEffect.SetNumber(nubmer);
            await textEffect.Run();
        }

        private async Task PerformSpeicalEffect(CreatureView creatureView, VisualEffectParam visualEffectParam)
        {
            var specialEffectParams = visualEffectParam.specialEffectParams;
            for (int i = 0; i < specialEffectParams.Count; i++)
            {
                await Task.Delay((specialEffectParams[i].showTimePoint * visualEffectParam.totalTime).ToMillisecond());
                var effect = Instantiate(specialEffectParams[i].EffectTempalte, transform);
                effect.transform.position = creatureView.transform.position + specialEffectParams[i].offsetFromSelf;
            }
        }

        private async Task PerformMoveEffect(CreatureView creatureView, VisualEffectParam visualEffectParam)
        {
            var moveParams = visualEffectParam.movePramas;
            var originPos = creatureView.transform.position;
            var waitTasks = new List<Task>();
            for (int i = 0; i < moveParams.Count; i++)
            {
                waitTasks.Add(Task.Delay((moveParams[i].moveTimePoint * visualEffectParam.totalTime).ToMillisecond()));
            }
            for (int i = 0; i < moveParams.Count; i++)
            {
                await waitTasks[i];
                await creatureView.transform.DOMove(originPos + creatureView.transform.right.x * moveParams[i].moveOffset, moveParams[i].time).AsyncWaitForCompletion();
            }
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

            if (visualEffectParam.specialEffectParams != null)
            {
                _ = PerformSpeicalEffect(creatureView, visualEffectParam);
            }

            if (visualEffectParam.movePramas != null)
            {
                _ = PerformMoveEffect(creatureView, visualEffectParam);
            }

            if (timePointCallbacks != null)
            {
                for (int i = 0; i < timePoints.Count; i++)
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