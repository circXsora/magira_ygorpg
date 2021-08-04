using MGO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class WorkTest
{
    [Test]
    public void NormalWorkTestPasses()
    {
        var workManager = new WorkManager();

        Vector3 result = Vector3.zero;

        Work work = SimpleWork.Create(() => { result = Vector3.one; });
        workManager.StartWork(work);
        workManager.Update(1, 1);
        Assert.That(result, Is.EqualTo(Vector3.one));

        Vector3 result2 = Vector3.zero;
        work = WaitSecondsWork.Create(5);
        work.ContinueWith(_ => { result2 = Vector3.one; });
        workManager.StartWork(work);
        workManager.Update(1, 1);
        workManager.Update(1, 1);
        Assert.That((work as WaitSecondsWork).LeftSeconds, Is.EqualTo(3));
        Assert.That(work.WorkStatus == WorkStatus.Running);
        workManager.Update(1, 1);
        workManager.Update(1, 1);
        workManager.Update(1, 1);
        Assert.That(result2, Is.EqualTo(Vector3.one));

        result = Vector3.zero;
        work = TimerWork.Create(0.5f, () => { result.x += 1; Debug.Log("Excute"); });
        workManager.StartWork(work);
        workManager.Update(0.5f, 0.5f);
        Assert.That(result.x, Is.EqualTo(1f));

        workManager.Update(1f, 1f);
        Assert.That(result.x, Is.EqualTo(3f));
    }
    [Test]
    public void SequenceWorkTestPasses()
    {
        var workManager = new WorkManager();

        int result = 0;
        var work1 = SimpleWork.Create(() => result += 1);
        work1.Name = "simple work 1";
        var work2 = WaitSecondsWork.Create(2);
        work2.Name = "wait work";
        var work3 = SimpleWork.Create(() => result += 4);
        work3.Name = "simple work 2";
        var sequcenceWork = SequenceWork.Create(work1, work2, work3);

        workManager.StartWork(sequcenceWork);
        workManager.Update(1, 1);
        Assert.That(sequcenceWork.WorkStatus == WorkStatus.Complete, Is.False);
        Assert.That(result, Is.EqualTo(1));
        Assert.That(sequcenceWork.CurrentWorkIndex, Is.EqualTo(1));
        Assert.That(sequcenceWork.CurrentWork.Name, Is.EqualTo("wait work"));
        workManager.Update(2, 2);
        Assert.That(sequcenceWork.CurrentWorkIndex, Is.EqualTo(2));
        Assert.That(sequcenceWork.CurrentWork.Name, Is.EqualTo("simple work 2"));
        workManager.Update(1, 1);
        Assert.That(result, Is.EqualTo(5));
    }
    [Test]
    public void ParallelWorkTestPasses()
    {
        var workManager = new WorkManager();

        int result = 0;
        var work1 = SimpleWork.Create(() => result += 1);
        work1.Name = "simple work 1";
        var work2 = WaitSecondsWork.Create(2);
        work2.Name = "wait work";
        work2.ContinueWith((_) => result += 1);
        var work3 = SimpleWork.Create(() => result += 4);
        work3.Name = "simple work 2";

        var paralleWork = ParallelWork.Create(work1, work2, work3);
        Assert.That(paralleWork.RunningWorks.Count, Is.EqualTo(3));

        workManager.StartWork(paralleWork);
        workManager.Update(1, 1);
        Assert.That(result, Is.EqualTo(5));
        Assert.That(paralleWork.RunningWorks.Count, Is.EqualTo(1));
        Assert.That(paralleWork.CompleteWorks.Count, Is.EqualTo(2));

        workManager.Update(1, 1);
        Assert.That(result, Is.EqualTo(6));
    }
    [Test]
    public void LerpWorkTestPasses()
    {
        var workManager = new WorkManager();

        var canvas = new GameObject().AddComponent<Canvas>();
        var image = new GameObject("image").AddComponent<Image>();
        image.transform.SetParent(canvas.transform);
        var rectTransform = image.GetComponent<RectTransform>();

        /// Anchored Position 
        rectTransform.anchoredPosition = Vector2.zero;
        var work = AnchoredPositionLerpWork.Create(rectTransform, Vector2.one, 3f);
        workManager.StartWork(work);
        workManager.Update(3, 3);
        Assert.That(rectTransform.anchoredPosition, Is.EqualTo(Vector2.one));

        // Local Scale
        rectTransform.localScale = Vector3.one;
        var work2 = LocalScaleLerpWork.Create(rectTransform, Vector3.zero, 1f);
        workManager.StartWork(work2);
        workManager.Update(1, 1);
        Assert.That(rectTransform.localScale, Is.EqualTo(Vector3.zero));

    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator NewTestScriptWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
