using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStatus : MonsterStatus
{
    private float HealValue;

    public override void Active(MonsterController effectedMonster)
    {
        Debug.Assert(TurnCount > 0);
        TurnCount--;
        effectedMonster.Heal(HealValue);
        BattleRecord.Instance.Log("由于受到回复状态的影响，这回合" + effectedMonster.Data.Info.MonsterName + "回复" + HealValue + "点生命值");

        if (TurnCount == 0)
        {
            BattleRecord.Instance.Log(effectedMonster.Data.Info.MonsterName + "回复状态结束");
            OnEffectOver();
        }
    }

    public HealStatus(float healVal, int turnCount) : base(turnCount)
    {
        this.HealValue = healVal;
    }
}
