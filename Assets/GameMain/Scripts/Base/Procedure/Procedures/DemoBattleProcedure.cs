//------------------------------------------------------------------------------
//  <copyright file="InitProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/20 23:41:25
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class DemoBattleProcedure : ProcedureBase
    {

        private EnvironmentEntity envirnment;
        private PlayerEntity player;
        private List<MonsterEntity> playerMonsters = new();
        private List<MonsterEntity> enemyMonsters = new();

        private int playerTurn;
        private int enemyTurn;

        public override async void OnEnter()
        {
            envirnment = await GameEntry.Environment.Load(EnvironmentType.Environment_1);
            _ = envirnment.Active();
            player = GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Player, party = CreaturesParty.Player }) as PlayerEntity;
            playerMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Player, entryId = 1 }) as MonsterEntity);
            playerMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Player, entryId = 2 }) as MonsterEntity);
            //playerMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Player, entryId = 3 }) as MonsterEntity);

            enemyMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Enemy, entryId = 4 }) as MonsterEntity);
            //enemyMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Enemy, entryId = 5 }) as MonsterEntity);
            //enemyMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, party = CreaturesParty.Enemy, entryId = 6 }) as MonsterEntity);

            var bindings = envirnment.GetComponentInChildren<EnvironmentBindings>();
            Debug.Assert(bindings != null);
            player.SetPoint( bindings.GetPlayerPoint(0));

            for (int i = 0; i < playerMonsters.Count; i++)
            {
                playerMonsters[i].SetPoint(bindings.GetPlayerMonsterPoint(i));
            }

            for (int i = 0; i < enemyMonsters.Count; i++)
            {
                enemyMonsters[i].SetPoint(bindings.GetEnemyPoint(i));
            }

            GameEntry.Card.CreateCards(5);

            base.OnEnter();

            StartPlayerTurn(true);
        }

        private void StartPlayerTurn(bool isFirstTurn)
        {
            foreach (var monster in playerMonsters)
            {
                monster.GetGearHolder().Get<EntitySelector>().OnBeginDragEvent += PlayerMonsterOnBeginDragEvent;
                monster.GetGearHolder().Get<EntitySelector>().OnDragEvent += PlayerMonsterOnDragEvent;
                monster.GetGearHolder().Get<EntitySelector>().OnEndDragEvent += PlayerMonsterOnEndDragEvent;
            }
        }

        private void EndPlayerTurn()
        {
            foreach (var monster in playerMonsters)
            {
                monster.GetGearHolder().Get<EntitySelector>().OnBeginDragEvent -= PlayerMonsterOnBeginDragEvent;
                monster.GetGearHolder().Get<EntitySelector>().OnDragEvent -= PlayerMonsterOnDragEvent;
                monster.GetGearHolder().Get<EntitySelector>().OnEndDragEvent -= PlayerMonsterOnEndDragEvent;
            }
        }

        private void PlayerMonsterOnEndDragEvent(object sender, PointerEventData e)
        {
            
        }

        private void PlayerMonsterOnDragEvent(object sender, PointerEventData e)
        {
            
        }

        private void PlayerMonsterOnBeginDragEvent(object sender, UnityEngine.EventSystems.PointerEventData e)
        {
            
        }
    }
}