using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameObjectPoolManager : MonoBehaviour
{
    public class PoolInfo
    {
        public PoolInfo(string poolName, string tagName) : this(poolName, tagName, null) { }

        public PoolInfo(string poolName, string tagName, GameObject prefab)
        {
            this.PoolName = poolName;
            this.TagName = tagName;
            this.Prefab = prefab;
        }

        /// <summary>
        /// 对象池名
        /// </summary>
        public string PoolName { get; set; }

        /// <summary>
        /// 对象池标签名
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 对象池实例化的预制体
        /// </summary>
        public GameObject Prefab { get; set; }

        /// <summary>
        /// TODO:是否记录在池外的对象 
        /// </summary>
        public bool RecordOutPoolObject { get; set; } = false;

        /// <summary>
        /// TODO:对象池自动释放间隔，如果设置为空，则不会自动释放
        /// </summary>
        public int? AutoReleaseInterval { get; set; }

        /// <summary>
        /// TODO:自动释放时保留的池对象的最小数量
        /// </summary>
        public int MinObjectCountAfterAutoRelease { get; set; }

        /// <summary>
        /// TODO:对象池对象的最大值，如果设置为空，则为无限，超出池大小的回收对象会被销毁
        /// </summary>
        public int? PoolMaxSize { get; set; }
    }

    /// <summary>
    /// 对象池总数量
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Key:对象池名字
    /// </summary>
    private class GameObjectPoolDictionary : Dictionary<string, GameObjectPool> { }

    /// <summary>
    /// 分标签对象池
    /// </summary>
    private Dictionary<string, GameObjectPoolDictionary> _tagedPools = new Dictionary<string, GameObjectPoolDictionary>();

    /// <summary>
    /// 对象池名字和对象池标签所对应的字典
    /// </summary>
    private Dictionary<string, string> _poolNameForPoolTagDictionary = new Dictionary<string, string>();

    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject Get(string poolName)
    {
        var pool = GetPool(poolName);
        if (pool == null)
        {
            Logger.Error($"对象池{poolName}尚未被注册，无法使用{nameof(Get)}方法");
            return null;
        }
        return pool.Get();
    }

    /// <summary>
    /// 回收对象到对象池
    /// </summary>
    public void Recycle(string poolName, GameObject obj)
    {
        var pool = GetPool(poolName);
        if (pool == null)
        {
            Logger.Error($"该对象池{poolName}尚未被注册，无法回收对象！");
            return;
        }
        pool.Recycle(obj);
    }

    /// <summary>
    /// 注册一个新的对象池
    /// </summary>
    public void RegisterNewPool(PoolInfo poolInfo)
    {
        #region 必须项检测
        if (poolInfo == null)
        {
            Logger.Error($"注册对象池时，对象池信息为空。");
            return;
        }
        if (string.IsNullOrEmpty(poolInfo.PoolName))
        {
            Logger.Error($"注册对象池时，对象池名字为空。");
            return;
        }
        if (string.IsNullOrEmpty(poolInfo.TagName))
        {
            Logger.Error($"注册对象池时，对象池{poolInfo.PoolName}标签为空。");
            return;
        }
        if (poolInfo.Prefab == null)
        {
            Logger.Warning($"注册对象池时，对象预设体为空，此对象池{poolInfo.PoolName}不会自动生成对象。");
        }
        #endregion

        if (IsRegisterPool(poolInfo.PoolName))
        {
            Logger.Error($"该对象池{poolInfo.PoolName}已经注册过了，请勿重复注册，可以执行{nameof(UnRegisterPool)}方法销毁已有对象池。");
            return;
        }

        _poolNameForPoolTagDictionary.Add(poolInfo.PoolName, poolInfo.TagName);

        GameObjectPoolDictionary pools;
        if (!_tagedPools.TryGetValue(poolInfo.TagName, out pools))
        {
            pools = new GameObjectPoolDictionary();
            _tagedPools.Add(poolInfo.TagName, pools);
        }
        GameObjectPool pool;
        if (poolInfo.RecordOutPoolObject)
        {
            pool = new RecordableGameObjectPool(poolInfo, transform);
        }
        else
        {
            pool = new GameObjectPool(poolInfo, transform);
        }
        pools.Add(poolInfo.PoolName, pool);
        Count++;
    }

    public bool IsRegisterPool(string poolName) => _poolNameForPoolTagDictionary.ContainsKey(poolName);

    /// <summary>
    /// 注销对象池
    /// </summary>
    public void UnRegisterPool(string poolName)
    {
        if (!IsRegisterPool(poolName))
        {
            Logger.Error($"该对象池{poolName}尚未被注册，无法被注销！");
            return;
        }
        if (_poolNameForPoolTagDictionary.TryGetValue(poolName, out string tagName))
        {
            if (_tagedPools.TryGetValue(tagName, out var pools))
            {
                if (pools.TryGetValue(poolName, out GameObjectPool pool))
                {
                    pool.ReleaseAll();
                    pools.Remove(poolName);
                    Count--;
                }
                if (pools.Count <= 0)
                {
                    _tagedPools.Remove(tagName);
                }
            }
            _poolNameForPoolTagDictionary.Remove(poolName);
        }
    }

    /// <summary>
    /// 根据对象池标签注销对象池
    /// </summary>
    public void UnRegisterPoolsByTag(string tagName)
    {
        if (_tagedPools.TryGetValue(tagName, out var pools))
        {
            var tempPools = pools.Values.ToArray();
            foreach (var pool in tempPools)
            {
                UnRegisterPool(pool.PoolName);
            }
            pools.Clear();
            _tagedPools.Remove(tagName);
        }
    }

    public int GetPoolsCountByTag(string tagName)
    {
        if (_tagedPools.TryGetValue(tagName, out var pools))
        {
            return pools.Count;
        }
        else
        {
            return 0;
        }
    }

    public GameObjectPool GetPool(string poolName)
    {
        if (_poolNameForPoolTagDictionary.TryGetValue(poolName, out string tagName))
        {
            if (_tagedPools.TryGetValue(tagName, out var pools))
            {
                if (pools.TryGetValue(poolName, out GameObjectPool pool))
                {
                    return pool;
                }
            }
        }
        return null;
    }

    public TPool GetPool<TPool>(string poolName) where TPool : GameObjectPool
    {
        return GetPool(poolName) as TPool;
    }
}