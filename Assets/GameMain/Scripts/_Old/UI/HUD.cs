using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MGO.SingletonInScene<HUD>
{
    public void RegisterPlayerMonster(MonsterController monster)
    {
        foreach (var hud in PlayerMonsterHUD)
        {
            if (!hud.IsRegister)
            {
                hud.RegisterDataProvider(monster);
                hud.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void RegisterEnemyMonster(MonsterController monster)
    {
        foreach (var hud in EnemyMonsterHUD)
        {
            if (!hud.IsRegister)
            {
                hud.RegisterDataProvider(monster);
                hud.gameObject.SetActive(true);
                break;
            }
        }
    }

    public MonsterHUD[] PlayerMonsterHUD;
    public MonsterHUD[] EnemyMonsterHUD;
}
