using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using GameFramework.Fsm;
using mgo;
using GameFramework.Scene;
using UnityGameFramework.Runtime;
using GameFramework.Event;

namespace ygo
{

    public class ProcedureChangeScene : ProcedureBase
    {

        private const int MenuSceneId = 1;

        private bool change_to_menu = false;
        private bool is_change_scene_complete = false;
        private int backgounrd_music_id = 0;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            is_change_scene_complete = false;

            MGOGameEntry.Event.Subscribe(UnityGameFramework.Runtime.LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            MGOGameEntry.Event.Subscribe(UnityGameFramework.Runtime.LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            MGOGameEntry.Event.Subscribe(UnityGameFramework.Runtime.LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            MGOGameEntry.Event.Subscribe(UnityGameFramework.Runtime.LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            // 停止所有声音
            MGOGameEntry.Sound.StopAllLoadingSounds();
            MGOGameEntry.Sound.StopAllLoadedSounds();

            // 隐藏所有实体
            MGOGameEntry.Entity.HideAllLoadingEntities();
            MGOGameEntry.Entity.HideAllLoadedEntities();

            // 卸载所有场景
            string[] loadedSceneAssetNames = MGOGameEntry.Scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                MGOGameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            // 还原游戏速度
            MGOGameEntry.Base.ResetNormalGameSpeed();

            int sceneId = procedureOwner.GetData<VarInt32>("NextSceneId");
            change_to_menu = sceneId == MenuSceneId;
            //IDataTable<DRScene> dtScene = MGOGameEntry.DataTable.GetDataTable<DRScene>();
            //DRScene drScene = dtScene.GetDataRow(sceneId);
            //Log.Info(drScene.AssetName);
            //if (drScene == null)
            //{
            //    Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
            //    return;
            //}
            MGOGameEntry.Scene.LoadScene("Assets/Scenes/Menu.unity", 10, this);
            //MGOGameEntry.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, this);
            //m_BackgroundMusicId = drScene.BackgroundMusicId;
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            MGOGameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            MGOGameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            MGOGameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            MGOGameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (!is_change_scene_complete)
            {
                return;
            }

            if (change_to_menu)
            {
                ChangeState<ProcedureMenu>(procedureOwner);
            }
            else
            {
                //ChangeState<ProcedureMain>(procedureOwner);
            }
        }

        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            var ne = (UnityGameFramework.Runtime.LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);


            is_change_scene_complete = true;
        }

        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            var ne = (UnityGameFramework.Runtime.LoadSceneFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
        }

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
            var ne = (UnityGameFramework.Runtime.LoadSceneUpdateEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
        }

        private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
        {
            var ne = (UnityGameFramework.Runtime.LoadSceneDependencyAssetEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
        }
    }

}