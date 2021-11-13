using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameObjectPoolTest
{
    // A Test behaves as an ordinary method
    //[Test]
    //public void ObjectPoolTestSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    /// <summary>
    /// ����GameObject����ز���
    /// </summary>
    [UnityTest]
    public IEnumerator NormalGameObjectPoolTestPasses()
    {
        #region ��ʼ��
        var load = Resources.LoadAsync<GameObject>("Ball");
        yield return load;
        Assert.That(load.asset, Is.Not.Null);
        var asset = load.asset as GameObject;

        var gameObject = new GameObject(nameof(GameObjectPoolManager));
        var poolManager = gameObject.AddComponent<GameObjectPoolManager>();
        #endregion

        /// ����ע������
        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball", "Game", asset));
        Assert.IsTrue(poolManager.IsRegisterPool("Ball"));
        Assert.AreEqual(poolManager.Count, 1);

        /// ���Ի�ö����
        var ballPool = poolManager.GetPool("Ball");
        var notExistPool = poolManager.GetPool("NotExist");
        Assert.That(ballPool, Is.Not.Null);
        Assert.That(notExistPool, Is.Null);

        /// ���Ի�ö���
        var ball = poolManager.Get("Ball");
        var notExist = poolManager.Get("NotExist");
        Assert.That(ball, Is.Not.Null);
        Assert.That(notExist, Is.Null);

        // �����ʹ��Ԥ�������ת������
        Assert.AreEqual(ball.transform.rotation, asset.transform.rotation);
        Assert.AreEqual(ball.transform.lossyScale, asset.transform.lossyScale);

        /// ���Ի��ն���
        poolManager.Recycle("Ball", ball);
        Assert.AreEqual(ballPool.InPoolCount, 1);

        Assert.Throws<System.ArgumentNullException>(() => poolManager.Recycle("Ball", null));

        /// �����������Ķ����
        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball", "Menu", asset));
        Assert.AreEqual(poolManager.Count, 1);

        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball2", "Menu", asset));
        Assert.IsTrue(poolManager.IsRegisterPool("Ball2"));
        Assert.AreEqual(poolManager.Count, 2);

        /// �����ǲ���ע�������
        poolManager.UnRegisterPool("Ball");
        Assert.IsFalse(poolManager.IsRegisterPool("Ball"));
        Assert.AreEqual(ballPool.InPoolCount, 0);
        Assert.AreEqual(poolManager.Count, 1);

        poolManager.UnRegisterPoolsByTag("Menu");
        Assert.AreEqual(poolManager.Count, 0);
        Assert.IsFalse(poolManager.IsRegisterPool("Ball2"));

    }

    /// <summary>
    /// �ɼ�¼�������Ķ���ز���
    /// </summary>
    [UnityTest]
    public IEnumerator RecordableGameObjectPoolTestPasses()
    {
        var load = Resources.LoadAsync<GameObject>("Ball");
        yield return load;
        Assert.That(load.asset, Is.Not.Null);
        var asset = load.asset as GameObject;

        var gameObject = new GameObject(nameof(GameObjectPoolManager));
        var poolManager = gameObject.AddComponent<GameObjectPoolManager>();
        var poolInfo = new GameObjectPoolManager.PoolInfo("Ball", "Game", asset)
        {
            RecordOutPoolObject = true
        };
        poolManager.RegisterNewPool(poolInfo);
        var ballPool = poolManager.GetPool("Ball");
        Assert.That(ballPool, Is.TypeOf<GameObjectPoolManager.RecordableGameObjectPool>());

        var recordableBallPool = ballPool as GameObjectPoolManager.RecordableGameObjectPool;

        Assert.That(recordableBallPool.OutPoolCount, Is.EqualTo(0));
        var ball = poolManager.Get("Ball");
        Assert.That(recordableBallPool.OutPoolCount, Is.EqualTo(1));

        poolManager.Recycle("Ball", ball);
        Assert.That(recordableBallPool.OutPoolCount, Is.EqualTo(0));
    }
}
