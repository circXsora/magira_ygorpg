using UnityEngine;
namespace MGO.Entity
{

    public interface IEntityView
    {
        bool Inited { get; }
        bool Active { get; }

        Vector3 Position { get; set; }
        Vector3 LocalPosition { get; set; }

        Quaternion Rotation { get; set; }
        Quaternion LocalRotation { get; set; }

        Vector3 EulerRotation { get; set; }
        Vector3 EulerLocalRotation { get; set; }

        Vector3 Scale { get; set; }
        Vector3 LocalScale { get; set; }

        void Init();

        void ToActive();

        void ToDeactive();

        void UpdateLogic(float logicDeltaTime, float realDeltaTime);

    }
}
