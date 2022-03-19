//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class CreatureFactory
    {
        public CreatureLogic CreateLogic(CreatureInfo info)
        {
            CreatureLogic logic;

            switch (info.type)
            {
                case CreaturesType.Monsters:
                    logic = new MonsterLogic(info);
                    break;
                case CreaturesType.Player:
                    logic = new PlayerLogic(info);
                    break;
                default:
                    throw new NotImplementedException("还没有这种类型的Logic");
            }
            return logic;

        }

        public CreatureEntity CreateEntity(CreatureInfo info)
        {
            var logic = CreateLogic(info);
            GameObject instance = null;
            CreatureEntity entity = null;
            switch (logic.Info.type)
            {
                case CreaturesType.Monsters:
                    if (info.party == CreaturesParty.Player)
                    {
                        instance = GameObject.Instantiate(GameEntry.Creatures.PlayerMonsterTemplate, GameEntry.Creatures.transform);
                    }
                    else
                    {
                        instance = GameObject.Instantiate(GameEntry.Creatures.EnemyMonsterTemplate, GameEntry.Creatures.transform);
                    }
                    entity = instance.GetOrAddComponent<MonsterEntity>();
                    break;
                case CreaturesType.Player:
                    instance = GameObject.Instantiate(GameEntry.Creatures.PlayerTemplate, GameEntry.Creatures.transform);
                    entity = instance.GetOrAddComponent<PlayerEntity>();
                    break;
                default:
                    Debug.LogError("创建失败！没有这种类型的Creature");
                    break;
            }
            if (entity != null)
            {
                entity.Logic = logic;

                var holder = entity.GetComponentHolder();

                var selection = holder.Add<UniverseEntitySelection>();
                selection.CanSelect = false;

                var materialChanger = holder.Add<MaterialChanger>();
                materialChanger.SetRenderer(entity.MainRenderer);

                var sprite = GameEntry.Config.sprite.GetMonsterSprite(info.entryId);
                var spRenderer = entity.MainRenderer as SpriteRenderer;
                if (spRenderer != null)
                {
                    spRenderer.sprite = sprite;
                }
            }
            else
            {
                Debug.LogError("Entity Is null!");
            }
            return entity;
        }
    }
}