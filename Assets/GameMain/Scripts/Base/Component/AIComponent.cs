using DG.Tweening;
using NodeCanvas.BehaviourTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{

    public class AIComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        private Dictionary<CreatureOldLogic, BehaviourTreeOwner> creatureAIDic = new Dictionary<CreatureOldLogic, BehaviourTreeOwner>();

        [SerializeField]
        private BehaviourTree normalEnemyAI;

        public BehaviourTreeOwner GetBehaviourTreeOwner(CreatureOldLogic logic)
        {
            if (creatureAIDic.TryGetValue(logic, out var btOwner))
            {
                return btOwner;
            }
            else
            {
                var agent = new GameObject(logic.Info.entryId.ToString()).AddComponent<BehaviourTreeOwner>();
                agent.enabled = false;
                agent.repeat = false;
                agent.transform.SetParent(transform);
                creatureAIDic.Add(logic, agent);
                return agent;
            }

        }

        public BehaviourTree GetBehaviourTreeGraph(CreatureOldLogic logic)
        {
            if (logic.Info.party == CreaturesParty.Enemy && logic.Info.type == CreaturesType.Monsters)
            {
                return normalEnemyAI;
            }
            else
            {
                return null;
            }
        }

    }
}