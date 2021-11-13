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
        /// �������
        /// </summary>
        public string PoolName { get; set; }

        /// <summary>
        /// ����ر�ǩ��
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// �����ʵ������Ԥ����
        /// </summary>
        public GameObject Prefab { get; set; }

        /// <summary>
        /// TODO:�Ƿ��¼�ڳ���Ķ��� 
        /// </summary>
        public bool RecordOutPoolObject { get; set; } = false;

        /// <summary>
        /// TODO:������Զ��ͷż�����������Ϊ�գ��򲻻��Զ��ͷ�
        /// </summary>
        public int? AutoReleaseInterval { get; set; }

        /// <summary>
        /// TODO:�Զ��ͷ�ʱ�����ĳض������С����
        /// </summary>
        public int MinObjectCountAfterAutoRelease { get; set; }

        /// <summary>
        /// TODO:����ض�������ֵ���������Ϊ�գ���Ϊ���ޣ������ش�С�Ļ��ն���ᱻ����
        /// </summary>
        public int? PoolMaxSize { get; set; }
    }

    /// <summary>
    /// �����������
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Key:���������
    /// </summary>
    private class GameObjectPoolDictionary : Dictionary<string, GameObjectPool> { }

    /// <summary>
    /// �ֱ�ǩ�����
    /// </summary>
    private Dictionary<string, GameObjectPoolDictionary> _tagedPools = new Dictionary<string, GameObjectPoolDictionary>();

    /// <summary>
    /// ��������ֺͶ���ر�ǩ����Ӧ���ֵ�
    /// </summary>
    private Dictionary<string, string> _poolNameForPoolTagDictionary = new Dictionary<string, string>();

    /// <summary>
    /// �Ӷ�����л�ȡ����
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject Get(string poolName)
    {
        var pool = GetPool(poolName);
        if (pool == null)
        {
            Logger.Error($"�����{poolName}��δ��ע�ᣬ�޷�ʹ��{nameof(Get)}����");
            return null;
        }
        return pool.Get();
    }

    /// <summary>
    /// ���ն��󵽶����
    /// </summary>
    public void Recycle(string poolName, GameObject obj)
    {
        var pool = GetPool(poolName);
        if (pool == null)
        {
            Logger.Error($"�ö����{poolName}��δ��ע�ᣬ�޷����ն���");
            return;
        }
        pool.Recycle(obj);
    }

    /// <summary>
    /// ע��һ���µĶ����
    /// </summary>
    public void RegisterNewPool(PoolInfo poolInfo)
    {
        #region ��������
        if (poolInfo == null)
        {
            Logger.Error($"ע������ʱ���������ϢΪ�ա�");
            return;
        }
        if (string.IsNullOrEmpty(poolInfo.PoolName))
        {
            Logger.Error($"ע������ʱ�����������Ϊ�ա�");
            return;
        }
        if (string.IsNullOrEmpty(poolInfo.TagName))
        {
            Logger.Error($"ע������ʱ�������{poolInfo.PoolName}��ǩΪ�ա�");
            return;
        }
        if (poolInfo.Prefab == null)
        {
            Logger.Warning($"ע������ʱ������Ԥ����Ϊ�գ��˶����{poolInfo.PoolName}�����Զ����ɶ���");
        }
        #endregion

        if (IsRegisterPool(poolInfo.PoolName))
        {
            Logger.Error($"�ö����{poolInfo.PoolName}�Ѿ�ע����ˣ������ظ�ע�ᣬ����ִ��{nameof(UnRegisterPool)}�����������ж���ء�");
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
    /// ע�������
    /// </summary>
    public void UnRegisterPool(string poolName)
    {
        if (!IsRegisterPool(poolName))
        {
            Logger.Error($"�ö����{poolName}��δ��ע�ᣬ�޷���ע����");
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
    /// ���ݶ���ر�ǩע�������
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