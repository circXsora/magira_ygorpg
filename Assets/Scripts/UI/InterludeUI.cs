using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class InterludeUI : Magia.SingletonInScene<InterludeUI>
{

    public TMPro.TMP_Text InterludeText;

    public Transform StartPoint, EndPoint;
    public float StartMoveTime = .1f, MidWaitTime = .8f, EndMoveTime = .1f;
    async public Task WaitInterlude(string text)
    {
        InterludeText.transform.position = StartPoint.position;
        InterludeText.text = text;
        InterludeText.enabled = true;
        InterludeText.transform.DOMove((StartPoint.position + EndPoint.position) / 2f, StartMoveTime);
        await Task.Delay((int)(StartMoveTime * 1000));

        await Task.Delay((int)(MidWaitTime * 1000));

        InterludeText.transform.DOMove(EndPoint.position, EndMoveTime);
        await Task.Delay((int)(EndMoveTime * 1000));
        InterludeText.enabled = false;
        await Task.Delay(1);
    }

    [ContextMenu("Test")]
    public async void Test()
    {
        await WaitInterlude("战斗开始");
    }
}
