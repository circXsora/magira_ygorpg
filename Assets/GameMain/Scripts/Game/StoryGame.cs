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
using System.Collections.Generic;
using System.Linq;
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

        private RoomData touchedRoomData;

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
            var te = e as PlayerTouchedMonsterEventArgs;
            touchedRoomData = te.roomData;
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

        #region 迷宫逻辑

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

        public void ShowMazeScene()
        {
            GameEntry.RoomManager.ShowAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(true);
            var camera = GameEntry.Entity.GetEntity(mazeCameraId.Value);
            camera.gameObject.SetActive(true);
        }

        public void HideMazeScene()
        {
            GameEntry.RoomManager.HideAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(false);
            var camera = GameEntry.Entity.GetEntity(mazeCameraId.Value);
            camera.gameObject.SetActive(false);
            GameEntry.Sound.StopMusic();
        }
        #endregion

        #region 战斗逻辑

        private List<BattleFieldMonsterData> enemyMonsterDatas = new List<BattleFieldMonsterData>();
        private List<BattleFieldMonsterData> myMonsterDatas = new List<BattleFieldMonsterData>();

        public void InitBattleScene()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, EntitySuccessHandler);

            GameEntry.Sound.PlayMusic(MusicID.Battle);
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            var playerData = (player.Logic as PlayerLogic).PlayerData;
            var battleCameraId = GameEntry.Entity.GenerateSerialId();
            var battleFieldId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowBattleField(new BattleFieldData(battleFieldId, 1));
            GameEntry.UI.OpenUIForm(AssetUtility.GetUIFormAsset("BattleForm"), "Battle", new BattleFormParams(playerData));
            var battlePlayerData = new BattleFieldPlayerData(GameEntry.Entity.GenerateSerialId(), 10000, battleFieldId);
            battlePlayerData.PointName = "PlayerPoint1";
            GameEntry.Entity.ShowBattleFieldPlayer(battlePlayerData);
            var entity = GameEntry.Entity.GetEntity(battlePlayerData.Id);
            GameEntry.Entity.ShowCamera(new CameraData(battleCameraId, 2));

            int i = 1;
            foreach (var monsterData in playerData.MonsterDatas)
            {
                var battlePlayerMonsterData = new BattleFieldMonsterData(GameEntry.Entity.GenerateSerialId(), monsterData.TypeId);
                battlePlayerMonsterData.OwnerId = battleFieldId;
                battlePlayerMonsterData.PointName = "PlayerMonsterPoint" + i++;
                myMonsterDatas.Add(battlePlayerMonsterData);
                GameEntry.Entity.ShowBattleFieldMonster(battlePlayerMonsterData);
            }

            i = 1;
            foreach (var enemyMonsterData in touchedRoomData.MonsterDatas)
            {
                var battleMonsterData = new BattleFieldMonsterData(GameEntry.Entity.GenerateSerialId(), enemyMonsterData.TypeId);
                battleMonsterData.OwnerId = battleFieldId;
                battleMonsterData.PointName = "EnemyMonsterPoint" + i++;
                enemyMonsterDatas.Add(battleMonsterData);
                GameEntry.Entity.ShowBattleFieldMonster(battleMonsterData);
            }
        }

        private void EntitySuccessHandler(object sender, GameEventArgs e)
        {
            var ne = e as ShowEntitySuccessEventArgs;
            var data = enemyMonsterDatas.Find(d => d.Id == ne.Entity.Id);
            if (data != null)
            {
                GameEntry.HPBar.ShowHPBar(ne.Entity.Logic, 1, 1);
            }
        }

        public void DestroyBattleScene()
        {
            GameEntry.Entity.HideEntity(battleCameraId.Value);
            battleCameraId = null;
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, EntitySuccessHandler);
        }
        #endregion

        #endregion
    }
}
