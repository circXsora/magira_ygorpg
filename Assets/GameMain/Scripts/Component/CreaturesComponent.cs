//------------------------------------------------------------------------------
//  <copyright file="CreaturesComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:57:05
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class CreatureInfo
    {
        public int id;
        public int entryId;
        public CreaturesType type;
    }

    public class MonsterInfo
    {
        public CreatureLogic owner;
    }

    public enum CreaturesType
    {
        Monsters, 
        Player
    }

	public class CreaturesComponent : UnityGameFramework.Runtime.GameFrameworkComponent
	{
        private static int idGenerator = 0;
        private readonly Dictionary<int, CreatureLogic> creaturesDic = new Dictionary<int, CreatureLogic>();
        private readonly CreatureFactory factory = new CreatureFactory();

        public async Task<CreatureLogic> Load(CreatureInfo info)
        {
            info.id = idGenerator++;
            var logic = await factory.Create(info);
            await logic.LoadView();
            creaturesDic.Add(info.id, logic);
            return logic;
        }

        public async Task Release(int id)
        {
            await Task.Run(() =>
            {
                if (creaturesDic.TryGetValue(id, out var creatureLogic))
                {
                    creaturesDic.Remove(id);
                    creatureLogic.DestroyView();
                }
            }
            );
        }
    }
}