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
                        var instance = GameObject.Instantiate(GameEntry.Creatures.MonsterTemplate, GameEntry.Creatures.transform);
                        view = instance.GetOrAddComponent<MonsterView>();
                        var sprite = GameEntry.Config.sprite.GetMonsterSprite(info.entryId);
                        view.Bindings.mainRenderer.sprite = sprite;
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