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
        private int? mazeCameraId;
        private int? battleCameraId;

        private Vector3 BattlePosition;
        private Vector3 MazePosition;

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
            mazeCameraId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowCamera(new CameraData(mazeCameraId.Value, 1));
            GameEntry.RoomManager.GenerateRooms();
        }

        public void HideMazeScene()
        {
            GameEntry.RoomManager.HideAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(false);
            var camera = GameEntry.Entity.GetEntity(mazeCameraId.Value);
            camera.gameObject.SetActive(false);
        }

        public void ShowMazeScene()
        {
            GameEntry.RoomManager.ShowAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(true);
            var camera = GameEntry.Entity.GetEntity(mazeCameraId.Value);
            camera.gameObject.SetActive(true);
        }

        public void InitBattleScene()
        {
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            var playerData = (player.Logic as Player).PlayerData;
            var battleCameraId = GameEntry.Entity.GenerateSerialId();
            var battleFieldId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowBattleField(new BattleFieldData(battleFieldId, 1));
            GameEntry.Entity.ShowCamera(new CameraData(battleCameraId, 2));
            GameEntry.UI.OpenUIForm(AssetUtility.GetUIFormAsset("BattleForm"), "Battle", new BattleFormParams(playerData));
            var battlePlayerData =  playerData.Clone() as PlayerData;
            GameEntry.Entity.ShowPlayer(battlePlayerData);
            var entity = GameEntry.Entity.GetEntity(battlePlayerData.Id);
            GameEntry.Entity.AttachEntity(battlePlayerData.Id, battleFieldId, "PlayerPoint1");
        }

        public void DestroyBattleScene()
        {
            GameEntry.Entity.HideEntity(battleCameraId.Value);
            battleCameraId = null;
        }
        #endregion
    }
}
