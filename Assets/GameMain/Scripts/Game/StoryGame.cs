//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using Stateless;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public class StoryGame : GameBase
    {

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Story;
            }
        }

        private int? videoFormId;
        private int? playerId;
        private int? cameraId;

        public override void Initialize()
        {
            base.Initialize();

            GameEntry.Event.Subscribe(OPPlayFinishEventArgs.EventId, OnOPPlayFinishHandler);
            GameEntry.Event.Subscribe(PlayerTouchedMonsterEventArgs.EventId, OnPlayerTouchedMonsterHanlder);

            videoFormId = GameEntry.UI.OpenUIForm(UIFormID.VideoForm);
        }
        public override void Shutdown()
        {
            GameEntry.Event.Unsubscribe(OPPlayFinishEventArgs.EventId, OnOPPlayFinishHandler);
            GameEntry.Event.Unsubscribe(PlayerTouchedMonsterEventArgs.EventId, OnPlayerTouchedMonsterHanlder);

            GameEntry.Sound.StopMusic();
            base.Shutdown();
        }
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }

        #region 事件处理
        private void OnPlayerTouchedMonsterHanlder(object sender, GameEventArgs e)
        {
            HideMazeScene();
            InitBattleScene();
        }

        private void OnOPPlayFinishHandler(object sender, GameEventArgs e)
        {
            GameEntry.UI.CloseUIForm(videoFormId.Value);
            GameEntry.Sound.PlayMusic(MusicID.Main);

            InitMazeScene();
        }
        #endregion

        #region 游戏逻辑
        public void InitMazeScene()
        {
            playerId = GameEntry.Entity.GenerateSerialId();
            var playerData = new PlayerData(playerId.Value, 10000);
            playerData.MonsterDatas = new MonsterData[] { new MonsterData(4, 3), new MonsterData(5) };
            GameEntry.Entity.ShowPlayer(playerData);
            cameraId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowCamera(new CameraData(cameraId.Value, 1));
            GameEntry.RoomManager.GenerateRooms();
        }

        public void HideMazeScene()
        {
            GameEntry.RoomManager.HideAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(false);
            var camera = GameEntry.Entity.GetEntity(cameraId.Value);
            camera.gameObject.SetActive(false);
        }

        public void ShowMazeScene()
        {
            GameEntry.RoomManager.ShowAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(true);
            var camera = GameEntry.Entity.GetEntity(cameraId.Value);
            camera.gameObject.SetActive(true);
        }

        public void InitBattleScene()
        {
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            
            GameEntry.UI.OpenUIForm(AssetUtility.GetUIFormAsset("BattleForm"), "Battle", new BattleFormParams((player.Logic as Player).MonsterDatas));
        }

        public void DestroyBattleScene()
        {

        }
        #endregion
    }
}
