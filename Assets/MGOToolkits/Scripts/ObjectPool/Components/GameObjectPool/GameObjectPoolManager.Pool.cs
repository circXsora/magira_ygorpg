using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameObjectPoolManager : MonoBehaviour
{
    public class GameObjectPool : ObjectPoolBase<GameObject>
    {
        public string TagName { get; }
        public GameObject Prefab { get; }
        public Transform Parent { get; }

        public GameObjectPool(PoolInfo poolInfo, Transform parent) : base(poolInfo.PoolName)
        {
            TagName = poolInfo.TagName;
            Prefab = poolInfo.Prefab;
            Parent = parent;
        }


        public override GameObject Spawn()
        {
            if (Prefab)
            {
                return Instantiate(Prefab);
            }
            else
            {
                return null;
            }
        }

        public override void Reset(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(Parent);
            obj.transform.localPosition = Vector3.zero;

            if (Prefab != null)
            {
                obj.transform.rotation = Prefab.transform.rotation;
                obj.transform.localScale = Prefab.transform.localScale;
            }
            else
            {
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
            }
        }

        public override void Release(GameObject obj)
        {
            Destroy(obj);
        }
    }

    /// <summary>
    /// 能记录正在使用对象的对象池
    /// </summary>
    public class RecordableGameObjectPool : GameObjectPool
    {
        private List<GameObject> _outPoolObjects = new List<GameObject>();

        /// <summary>
        /// 对象池外对象
        /// </summary>
        public int OutPoolCount => _outPoolObjects.Count;

        public RecordableGameObjectPool(PoolInfo poolInfo, Transform parent) : base(poolInfo, parent)
        {
        }

        public override GameObject Get()
        {
            var obj = base.Get();

            if (obj != null)
            {
                _outPoolObjects.Add(obj);
            }

            return obj;
        }

        public override void Recycle(GameObject obj)
        {
            base.Recycle(obj);
            _outPoolObjects.Remove(obj);
        }
    }
}
