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
    /// 常规GameObject对象池测试
    /// </summary>
    [UnityTest]
    public IEnumerator NormalGameObjectPoolTestPasses()
    {
        #region 初始化
        var load = Resources.LoadAsync<GameObject>("Ball");
        yield return load;
        Assert.That(load.asset, Is.Not.Null);
        var asset = load.asset as GameObject;

        var gameObject = new GameObject(nameof(GameObjectPoolManager));
        var poolManager = gameObject.AddComponent<GameObjectPoolManager>();
        #endregion

        /// 测试注册对象池
        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball", "Game", asset));
        Assert.IsTrue(poolManager.IsRegisterPool("Ball"));
        Assert.AreEqual(poolManager.Count, 1);

        /// 测试获得对象池
        var ballPool = poolManager.GetPool("Ball");
        var notExistPool = poolManager.GetPool("NotExist");
        Assert.That(ballPool, Is.Not.Null);
        Assert.That(notExistPool, Is.Null);

        /// 测试获得对象
        var ball = poolManager.Get("Ball");
        var notExist = poolManager.Get("NotExist");
        Assert.That(ball, Is.Not.Null);
        Assert.That(notExist, Is.Null);

        // 对象会使用预制体的旋转和缩放
        Assert.AreEqual(ball.transform.rotation, asset.transform.rotation);
        Assert.AreEqual(ball.transform.lossyScale, asset.transform.lossyScale);

        /// 测试回收对象
        poolManager.Recycle("Ball", ball);
        Assert.AreEqual(ballPool.InPoolCount, 1);

        Assert.Throws<System.ArgumentNullException>(() => poolManager.Recycle("Ball", null));

        /// 不允许重名的对象池
        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball", "Menu", asset));
        Assert.AreEqual(poolManager.Count, 1);

        poolManager.RegisterNewPool(new GameObjectPoolManager.PoolInfo("Ball2", "Menu", asset));
        Assert.IsTrue(poolManager.IsRegisterPool("Ball2"));
        Assert.AreEqual(poolManager.Count, 2);

        /// 以下是测试注销对象池
        poolManager.UnRegisterPool("Ball");
        Assert.IsFalse(poolManager.IsRegisterPool("Ball"));
        Assert.AreEqual(ballPool.InPoolCount, 0);
        Assert.AreEqual(poolManager.Count, 1);

        poolManager.UnRegisterPoolsByTag("Menu");
        Assert.AreEqual(poolManager.Count, 0);
        Assert.IsFalse(poolManager.IsRegisterPool("Ball2"));

    }

    /// <summary>
    /// 可记录池外对象的对象池测试
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
