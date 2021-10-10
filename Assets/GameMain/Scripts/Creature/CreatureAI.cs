//------------------------------------------------------------------------------
//  <copyright file="CreatureAI.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/10 9:52:02
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using NodeCanvas.BehaviourTrees;
using System.Threading.Tasks;

namespace BBYGO
{
    public class CreatureAI
    {
        public CreatureLogic owner;

        private BehaviourTreeOwner behaviourTreeOwner;
        private BattleContext battle;


        public CreatureAI(CreatureLogic owner)
        {
            this.owner = owner;
        }

        public async Task Run()
        {
            if (behaviourTreeOwner == null)
            {
                behaviourTreeOwner = GameEntry.AI.GetBehaviourTreeOwner(owner);
            }
            var grahp = GameEntry.AI.GetBehaviourTreeGraph(owner);
            grahp.repeat = false;
            battle = GameEntry.Context.Battle;
            battle.pointerClickedMonster = owner.View.gameObject;
            battle.selectMonsters.Clear();
            battle.selectMonsters.Add(battle.playerMonsters[0].View.gameObject);
            behaviourTreeOwner.StartBehaviour(grahp);
            await TaskExtensions.WaitUntil(() => grahp.rootStatus != NodeCanvas.Framework.Status.Running);
        }
    }
}