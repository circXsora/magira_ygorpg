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
            GameEntry.RoomManager.HideAllRooms();
            var player = GameEntry.Entity.GetEntity(playerId.Value);
            player.gameObject.SetActive(false);
            Log.Info("隐藏当前信息");
        }

        private void OnOPPlayFinishHandler(object sender, GameEventArgs e)
        {
            GameEntry.UI.CloseUIForm(videoFormId.Value);
            GameEntry.Sound.PlayMusic(MusicID.Main);

            playerId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowPlayer(new PlayerData(playerId.Value, 60000));
            GameEntry.RoomManager.GenerateRooms();
        }
        #endregion


    }
}
