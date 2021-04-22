//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using Stateless;
using UnityEngine;

namespace bbygo
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



        private enum State
        {
            Empty,
            Maze,
            Battle,
        }

        private enum Trigger
        {
            BattleStart,
            BattleEnd,
        }

        public RoomController CurrentRoomCtrl, BossRoomCtrl;

        private StateMachine<State, Trigger> machine;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
