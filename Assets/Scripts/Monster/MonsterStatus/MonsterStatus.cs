using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class MonsterStatus
{
    protected MonsterStatus(int turnCount)
    {
        this.TurnCount = turnCount;
    }

    public event EventHandler EffectOver;
    protected void OnEffectOver()
    {
        EffectOver?.Invoke(this, null);
    }

    public int TurnCount;
    public abstract void Active(MonsterController effectedMonster);
    public virtual async Task ActiveAsync(MonsterController effectedMonster) { await Task.Yield(); Active(effectedMonster); }
}
