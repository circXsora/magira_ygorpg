//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
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
        public CreatureView CreateView(CreatureInfo info)
        {
            CreatureView view;

            switch (info.type)
            {
                case CreaturesType.Monsters:
                    {
                        GameObject instance = null;
                        switch (info.party)
                        {
                            case CreaturesParty.Player:
                                instance = GameObject.Instantiate(GameEntry.Creatures.PlayerMonsterTemplate, GameEntry.Creatures.transform);
                                break;
                            case CreaturesParty.Enemy:
                                instance = GameObject.Instantiate(GameEntry.Creatures.EnemyMonsterTemplate, GameEntry.Creatures.transform);
                                break;
                            default:
                                throw new InvalidOperationException("没有这种阵营");
                        }
                        
                        var monsterView = instance.GetOrAddComponent<MonsterView>();
                        view = monsterView;

                        monsterView.MonsterUI = GameObject.Instantiate(GameEntry.Creatures.MonsterUITemplate, GameEntry.Creatures.transform).GetComponent<MonsterUI>();
                        monsterView.MonsterUI.transform.rotation = Quaternion.identity;
                        monsterView.MonsterUI.gameObject.AddComponent<TragetTracker>().Target = monsterView.Bindings.MonsterUIPoint;
                        var sprite = GameEntry.Config.sprite.GetMonsterSprite(info.entryId);
                        try
                        {
                            view.Bindings.mainRenderer.sprite = sprite;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    break;
                case CreaturesType.Player:
                    {
                        var instance = GameObject.Instantiate(GameEntry.Creatures.PlayerTemplate, GameEntry.Creatures.transform);
                        view = instance.GetOrAddComponent<PlayerView>();
                    }
                    break;
                default:
                    throw new NotImplementedException("还没有这种类型的View");
            }
            view.Info = info;
            return view;
        }

        public CreatureLogic CreateLogic(CreatureInfo info)
        {
            CreatureLogic logic;

            switch (info.type)
            {
                case CreaturesType.Monsters:
                    logic = new MonsterLogic(info);
                    return logic;
                case CreaturesType.Player:
                    logic = new PlayerLogic(info);
                    return logic;
                default:
                    throw new NotImplementedException("还没有这种类型的Logic");
            }
        }
    }
}