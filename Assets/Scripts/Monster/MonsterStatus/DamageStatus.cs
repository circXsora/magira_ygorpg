using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DamageStatus : MonsterStatus
{
    private float DamageValue;

    public override void Active(MonsterController effectedMonster)
    {
        Debug.Assert(TurnCount > 0);
        TurnCount--;
        effectedMonster.TakeDamage(DamageValue);
        BattleRecord.Instance.Log("由于受到伤害状态的影响，这回合" + effectedMonster.Data.Info.MonsterName + "受到" + DamageValue + "点伤害");

        if (TurnCount == 0)
        {
            BattleRecord.Instance.Log(effectedMonster.Data.Info.MonsterName + "伤害状态结束");
            OnEffectOver();
        }
    }

    public DamageStatus(float damageVal, int turnCount) : base(turnCount)
    {
        this.DamageValue = damageVal;
    }
}
