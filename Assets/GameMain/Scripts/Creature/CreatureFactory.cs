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
        internal CreatureLogic Create(CreatureInfo info)
        {
            CreatureLogic logic;

            switch (info.type)
            {
                case CreaturesType.Monsters:
                    logic = new MonsterLogic(info);
                    logic.LoadView += async () =>
                    {
                        var sb = new StringBuilder();
                        var spriteTask = GameEntry.Resource.LoadAsync<Sprite>("Textures/" + info.type.ToString() + "/" + info.type.ToString() + "_" + info.entryId);
                        var instance = await LoadTemplateInstance();
                        var creatureBindings = instance.GetComponent<CreatureBindings>();
                        creatureBindings.mainRenderer.sprite = await spriteTask;
                        logic.SetView(instance.GetOrAddComponent<PlayerView>());
                    };
                    return logic;

                case CreaturesType.Player:
                    logic = new PlayerLogic(info);
                    logic.LoadView += async () =>
                    {
                        logic.SetView((await LoadTemplateInstance()).GetOrAddComponent<PlayerView>());
                    };
                    return logic;
                default:
                    return null;
            }

            async Task<GameObject> LoadTemplateInstance()
            {
                var reource = await GameEntry.Resource.LoadAsync<GameObject>("CreatureTemplates/" + info.type.ToString());
                var instance = UnityEngine.Object.Instantiate(reource, GameEntry.Creatures.transform);
                return instance;
            }
        }
    }
}