//------------------------------------------------------------------------------
//  <copyright file="LaunchProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/4 23:05:37
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using GameFramework.Event;

namespace BBYGO
{
    public class MenuProcedure : SoraProcedureBase
    {

        public override async void OnEnter()
        {
            base.OnEnter();
            GameEntry.Event.Subscribe(GameStartButtonClickedEventArgs.EventId, OnGameStartButtonClicked);
            await GameEntry.UI.Open(UIType.MenuForm);
        }

        private async void OnGameStartButtonClicked(object sender, GameEventArgs e)
        {
            await GameEntry.UI.Close(UIType.MenuForm);
            ChangeState<GameProcedure>();
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate( elapseSeconds, realElapseSeconds);
        }

        public override void OnLeave(bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(GameStartButtonClickedEventArgs.EventId, OnGameStartButtonClicked);
            base.OnLeave(isShutdown);
        }
    }
}