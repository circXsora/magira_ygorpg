using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace MGO.Entity.Unity
{

    [Serializable]
    public class PointInfo
    {
        public Transform transform;
    }

    public class Entity : MonoBehaviour, IEntity
    {
        private EntityGearHolder entityGearHolder;

        public virtual Renderer MainRenderer { get; protected set; }

        public EntityLogic Logic { get; set; }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = new Vector3(value.x, value.y, value.z);
        }

        public Vector3 LocalPosition
        {
            get => transform.localPosition;
            set => transform.localPosition = new Vector3(value.x, value.y, value.z);
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Quaternion LocalRotation
        {
            get => transform.localRotation;
            set => transform.localRotation = value;
        }

        public Vector3 Scale
        {
            get => transform.lossyScale;
            set
            {
                transform.localScale = transform.parent == null ? value : transform.parent.worldToLocalMatrix.MultiplyPoint(value);
            }
        }

        public Vector3 LocalScale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        public Vector3 EulerRotation
        {
            get => transform.rotation.eulerAngles;
            set => transform.rotation = Quaternion.Euler(value);
        }

        public Vector3 EulerLocalRotation
        {
            get => transform.localRotation.eulerAngles;
            set => transform.localRotation = Quaternion.Euler(value);
        }

        public virtual bool Inited { get; private set; } = false;

        public virtual bool IsActive => gameObject.activeSelf;

        public virtual void Init()
        {
            if (Logic == null)
            {
                Debug.LogError("初始化失败！逻辑还未初始化！", this);
            }
            Inited = true;
        }

        public virtual async Task Active()
        {
            gameObject.SetActive(true);
        }

        public virtual async Task Deactive()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            if (!Inited)
                return;
            Logic.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        public void SetPoint(PointInfo pointInfo)
        {
            Position = pointInfo.transform.position;
        }

        public EntityGearHolder GetGearHolder()
        {
            if (entityGearHolder == null)
            {
                entityGearHolder = gameObject.AddComponent<EntityGearHolder>();
                entityGearHolder.Owner = this;
            }
            return entityGearHolder;
        }

        public ILogic GetLogic()
        {
            return Logic;
        }

    }
}