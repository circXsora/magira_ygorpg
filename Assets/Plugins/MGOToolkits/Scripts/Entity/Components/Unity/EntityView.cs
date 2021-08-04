using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MGO.Entity.Unity
{
    public class EntityView : MonoBehaviour, IEntityView
    {
        protected IEntityViewBinder _entityViewBinder;

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

        public virtual bool Active => gameObject.activeSelf;

        public void ToActive()
        {
            gameObject.SetActive(true);
        }

        public void ToDeactive()
        {
            gameObject.SetActive(false);
        }

        public virtual void UpdateLogic(float logicDeltaTime, float realDeltaTime)
        {

        }

        public virtual void Init()
        {
            Inited = true;
        }
    }
}