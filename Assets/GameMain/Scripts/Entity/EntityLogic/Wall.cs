using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public class Wall : UniversalEntityLogic
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            WallData _data = userData as WallData;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            WallData _data = userData as WallData;
            GameEntry.Entity.AttachEntity(_data.Id, _data.OwnerID.Value);
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);

            Name = string.Format("Wall of {0}", parentEntity.Name);
            CachedTransform.localPosition = Vector3.zero;
            CachedTransform.localScale = Vector3.one;
            CachedTransform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
