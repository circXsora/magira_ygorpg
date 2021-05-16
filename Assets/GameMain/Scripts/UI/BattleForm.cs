//------------------------------------------------------------------------------
//  <copyright file="BattleForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/15 18:53:44
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
namespace BBYGO
{
    using MGO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityGameFramework.Runtime;

    public class BattleForm : UGuiForm
    {
        [SerializeField] private GameObject _playerHUDGroup;
        public GameObject PlayerHUDGroup { get => _playerHUDGroup; set => _playerHUDGroup = value; }

        BattleFormParams _battleFormParams;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _battleFormParams = userData as BattleFormParams;
            Log.Info(_battleFormParams.PlayerMonsterDatas);
            var getNextCell = UguiUtility.CreateCellGenerator(PlayerHUDGroup.transform.GetChild(0).gameObject, PlayerHUDGroup.transform);
            var cell = getNextCell();
            Log.Info(cell);
        }
    }
}