using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using mgo;

namespace ygo
{
    public class ProcedurePreload : ProcedureBase
    {
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            procedureOwner.SetData<VarInt32>("NextSceneId", 1);
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
    }
}