using System;
using System.Threading.Tasks;
using UnityEngine;
namespace MGO.Entity
{
    public interface IEntity
    {
        bool Inited { get; }
        bool IsActive { get; }

        Vector3 Position { get; set; }
        Vector3 LocalPosition { get; set; }

        Quaternion Rotation { get; set; }
        Quaternion LocalRotation { get; set; }

        Vector3 EulerRotation { get; set; }
        Vector3 EulerLocalRotation { get; set; }

        Vector3 Scale { get; set; }
        Vector3 LocalScale { get; set; }

        void Init();

        Task Active();

        Task Deactive();

        ILogic GetLogic();
    }
}
