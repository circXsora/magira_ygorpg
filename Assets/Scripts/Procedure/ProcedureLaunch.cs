using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using mgo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace ygo
{

    public class ProcedureLaunch : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            string welcomeMessage = Utility.Text.Format("Hello Game Framework {0}.", Version.GameFrameworkVersion);

            // 打印调试级别日志，用于记录调试类日志信息
            //Log.Debug(welcomeMessage);

            // 打印信息级别日志，用于记录程序正常运行日志信息
            Log.Info(welcomeMessage);

            MGOGameEntry.Base.EditorResourceMode = true;

            // 打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用
            //Log.Warning(welcomeMessage);

            // 打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用
            //Log.Error(welcomeMessage);

            // 打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架
            //Log.Fatal(welcomeMessage);
        }
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // 运行一帧即切换到 Splash 展示流程
            ChangeState<ProcedureSplash>(procedureOwner);
        }

    }

}