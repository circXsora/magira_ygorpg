using GameFramework;
using GameFramework.Fsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mgo;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace ygo
{

    public class ProcedureSplash : ProcedureBase
    {
        private float current_play_time = 0f;
        private float play_time = 2f;
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // TODO: 这里可以播放一个 Splash 动画
            // ...
            // mgo.MGOGameEntry.UI.
            if (current_play_time < play_time)
            {
                current_play_time += elapseSeconds;
                return;
            }

            //if (mgo.GameEntry.Base.EditorResourceMode)
            //{
            //    // 编辑器模式
            //    Log.Info("Editor resource mode detected.");
            //    ChangeState<ProcedurePreload>(procedureOwner);
            //}
            //else if (mgo.GameEntry.Resource.ResourceMode == ResourceMode.Package)
            //{
            //    // 单机模式
            //    Log.Info("Package resource mode detected.");
            //    ChangeState<ProcedureInitResources>(procedureOwner);
            //}
            //else
            //{
            //    // 可更新模式
            //    Log.Info("Updatable resource mode detected.");
            //    ChangeState<ProcedureCheckVersion>(procedureOwner);
            //}

            Log.Info("Editor resource mode detected.");
            ChangeState<ProcedurePreload>(procedureOwner);
        }
    }

}