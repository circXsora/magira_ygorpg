using System.Collections.Generic;

public interface IObjectPool<T>
{
    /// <summary>
    /// 从池子中拿到对象
    /// </summary>
    T Get();

    /// <summary>
    /// 回收对象
    /// </summary>
    void Recycle(T obj);

    /// <summary>
    /// 回收对象时重置对象
    /// </summary>
    /// <param name="obj"></param>
    void Reset(T obj);

    /// <summary>
    /// 生成对象
    /// </summary>
    T Spawn();

    /// <summary>
    /// 销毁对象
    /// </summary>
    void Release(T obj);
}

public abstract class ObjectPoolBase<T> : IObjectPool<T> where T : class
{
    private Stack<T> _inPoolObjects = new Stack<T>();

    public int InPoolCount => _inPoolObjects.Count;

    public string PoolName { get; }

    public ObjectPoolBase(string poolName)
    {
        PoolName = poolName;
    }

    public virtual T Get()
    {
        //结果对象
        T result = null;

        //对象池里有对象
        while (result == null && _inPoolObjects.Count > 0)
        {
            result = _inPoolObjects.Pop();
        }

        if (result == null)
        {
            result = Spawn();
        }

        return result;
    }

    public virtual void Recycle(T obj)
    {
        if (obj == null)
        {
            throw new System.ArgumentNullException(nameof(obj), $"在对象池{PoolName}的{nameof(Recycle)}方法中，回收的对象为空。");
        }


        if (_inPoolObjects.Contains(obj))
        {
            Logger.Error($"该物体{obj}已经被加入到对象池中");
            return;
        }

        Reset(obj);

        _inPoolObjects.Push(obj);
    }

    public abstract void Reset(T obj);

    public abstract T Spawn();

    public abstract void Release(T obj);

    public void ReleaseAll()
    {
        while (_inPoolObjects.Count > 0)
        {
            Release(_inPoolObjects.Pop());
        }
        _inPoolObjects.Clear();
    }
}
