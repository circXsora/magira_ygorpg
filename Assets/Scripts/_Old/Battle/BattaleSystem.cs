using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class BattaleSystem : Magia.SingletonInScene<BattaleSystem>
{
    public UnityEvent OnWin, OnLose;

    bool IsSkill = false;
    BattleInfo.AttackInfo AttackInfo = new BattleInfo.AttackInfo();
    BattleInfo.SkillProcessInfo SkillProcessInfo = new BattleInfo.SkillProcessInfo();

    public List<MonsterController> SelectingRange = new List<MonsterController>();
    public MonsterController[] PlayerMonsterTemplates;
    public MonsterController[] EnemyMonsterTemplates;

    private List<MonsterController> PlayerMonsters = new List<MonsterController>();
    private List<MonsterController> EnemyMonsters = new List<MonsterController>();

    private MonsterController active_monster;
    private MonsterController ActiveMonster
    {
        get => active_monster;
        set
        {
            if (value == null)
            {
                BattleMenu.gameObject.SetActive(false);
            }
            else
            {
                BattleMenu.transform.position = value.transform.Find("MenuPoint").position;
                BattleMenu.SetSkillDataProvider(value);
                BattleMenu.gameObject.SetActive(true);
            }
            active_monster = value;
        }
    }

    public BattleMenu BattleMenu;
    public BattleRecord BattleRecord;

    public LayerMask MonsterLayer;

    #region FSM
    enum State
    {
        Start,

        Battle,

        PlayerTurn,
        Idle,

        SelectingTarget,
        Attacking,


        EnemyTurn,

        Win,
        Lose,
        SkillProcessing,
    }
    enum Trigger
    {
        Start,
        TurnEnd,
        Win,
        AttackCommand,
        Cancle,
        Attack,
        Click,
        Enter,
        Exit,
        AttackOver,
        Idle,
        DefenseCommand,
        SkillProcess,
        Die,
        Lose,
        SpecialSkillTrigger,
        BattleSkillTrigger,
        SkillProcessOver,
    }

    Stateless.StateMachine<State, Trigger> state_machine;

    Stateless.StateMachine<State, Trigger>.TriggerWithParameters<MonsterController>
        AttackEnemyTrigger,
        //SkillOnTargetTrigger,
        MonsterDieTrigger,
        MonsterClickedTrigger, MouseEnterMonsterTrigger, MouseExitMonsterTrigger;

    Stateless.StateMachine<State, Trigger>.TriggerWithParameters<SpecialSkill> SpecialSkillTrigger;
    Stateless.StateMachine<State, Trigger>.TriggerWithParameters<BattleSKill> BattleSkillTrigger;
    #endregion

    #region Init
    private void InitStateMachine()
    {
        state_machine = new Stateless.StateMachine<State, Trigger>(State.Start);


        //SkillOnTargetTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.SkillProcess);
        AttackEnemyTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.Attack);
        MonsterDieTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.Die);
        MonsterClickedTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.Click);
        MouseEnterMonsterTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.Enter);
        MouseExitMonsterTrigger = state_machine.SetTriggerParameters<MonsterController>(Trigger.Exit);
        SpecialSkillTrigger = state_machine.SetTriggerParameters<SpecialSkill>(Trigger.SpecialSkillTrigger);
        BattleSkillTrigger = state_machine.SetTriggerParameters<BattleSKill>(Trigger.BattleSkillTrigger);

        // Start
        state_machine.Configure(State.Start).Permit(Trigger.Start, State.Battle);

        // Battle
        state_machine.Configure(State.Battle)
            .OnEntry(async () =>
            {
                await InterludeUI.Instance.WaitInterlude("战斗开始");
                state_machine.Fire(Trigger.Start);
            })
            .Permit(Trigger.Win, State.Win)
            .Permit(Trigger.Lose, State.Lose)
            .Permit(Trigger.Start, State.PlayerTurn)
            .InternalTransition<MonsterController>(Trigger.Die, (t) =>
            {
                var monster = (MonsterController)t.Parameters[0];
                BattleRecord.Instance.Log(monster.Data.Info.MonsterName + " 死亡");
                if (monster.IsMyMonster)
                {
                    PlayerMonsters.Remove(monster);
                }
                else
                {
                    EnemyMonsters.Remove(monster);
                }
                monster.gameObject.SetActive(false);
                CheckState();
            })
            ;

        // Battle/PlayerTurn
        state_machine.Configure(State.PlayerTurn).SubstateOf(State.Battle)
            .OnEntry(async () =>
            {
                await Task.Delay(1000);
                await TalkUI.Instance.Open("我的回合!抽牌!", YuGiAvatarName.MyTurnDora);
                await InterludeUI.Instance.WaitInterlude("我的回合");
                PlayerMonsters.ForEach(m =>
                {
                    m.MonsterActive = true;
                    m.Active();
                });

                foreach (var mons in PlayerMonsters)
                {
                    await mons.ActiveStatuses();
                }
                await TalkUI.Instance.Close();
                state_machine.Fire(Trigger.Idle);
            })
            .Permit(Trigger.TurnEnd, State.EnemyTurn)
            .Permit(Trigger.Idle, State.Idle)
            ;

        // Battle/PlayerTurn/Idle
        state_machine.Configure(State.Idle).SubstateOf(State.PlayerTurn)
            .OnEntry(() =>
                        {
                            if (PlayerMonsters.All(m => m.MonsterActive == false))
                                state_machine.Fire(Trigger.TurnEnd);
                        })
            .OnExit(() => BattleMenu.gameObject.SetActive(false))
            .PermitDynamic(SpecialSkillTrigger, (spsk) =>
                        {
                            if (spsk.MonsterCount == 1)
                            {
                                // Single
                                if ((spsk.Range & Skill.SkillRange.Enemies) != 0)
                                {
                                    IsSkill = true;
                                    SkillProcessInfo.Skill = spsk;
                                    SelectingRange = EnemyMonsters.ToList();
                                    return State.SelectingTarget;
                                }
                            }
                            else
                            {
                                // Group
                            }
                            return State.Idle;
                        })
            .PermitDynamic(BattleSkillTrigger, (btsk) =>
                        {
                            IsSkill = true;
                            SkillProcessInfo.Skill = btsk;
                            SkillProcessInfo.ActiveMonster = ActiveMonster;
                            if (btsk.SelectingTarget)
                            {
                                if (btsk.MonsterCount == 1)
                                {
                                    // Single
                                    if ((btsk.Range & Skill.SkillRange.Enemies) != 0)
                                    {
                                        SelectingRange = EnemyMonsters.ToList();
                                        return State.SelectingTarget;
                                    }
                                    else if ((btsk.Range & Skill.SkillRange.Friends) != 0 && (btsk.Range & Skill.SkillRange.Self) != 0)
                                    {
                                        SelectingRange = PlayerMonsters.ToList();
                                        return State.SelectingTarget;
                                    }
                                }
                                else
                                {
                                    // Group

                                }
                            }
                            else
                            {
                                // 不取对象，即范围内所有对象
                                SkillProcessInfo.FunctionOnMonsters.Clear();
                                if ((btsk.Range & Skill.SkillRange.Enemies) != 0)
                                {
                                    SkillProcessInfo.FunctionOnMonsters.AddRange(EnemyMonsters.ToList());
                                }
                                if ((btsk.Range & Skill.SkillRange.Friends) != 0)
                                {
                                    SkillProcessInfo.FunctionOnMonsters.AddRange(PlayerMonsters.ToList());
                                    if ((btsk.Range & Skill.SkillRange.Self) == 0)
                                    {
                                        SkillProcessInfo.FunctionOnMonsters.Remove(SkillProcessInfo.ActiveMonster);
                                    }
                                }

                                return State.SkillProcessing;
                            }
                            return State.Idle;
                        })
            .PermitDynamic(Trigger.AttackCommand, () =>
                        {
                            IsSkill = false;
                            SelectingRange = EnemyMonsters.ToList();
                            return State.SelectingTarget;
                        })
            .PermitReentry(Trigger.Idle)
            .InternalTransition(Trigger.DefenseCommand, t =>
                        {
                            ActiveMonster.Defense();
                            ActiveMonster.MonsterActive = false;
                            ActiveMonster.BeNormal();
                            ActiveMonster = null;
                            state_machine.Fire(Trigger.Idle);
                        })
            .InternalTransition<MonsterController>(Trigger.Click, (t) =>
                        {
                            var monster = (MonsterController)t.Parameters[0];
                            if (!monster.IsMyMonster || !monster.MonsterActive)
                            {
                                return;
                            }
                            if (monster != ActiveMonster)
                            {
                                ActiveMonster?.UnSelecting();
                            }
                            ActiveMonster = monster;
                            ActiveMonster.Selecting();
                        })
            .InternalTransition<MonsterController>(Trigger.Enter, (t) =>
                        {
                            var monster = (MonsterController)t.Parameters[0];
                            if (monster.IsMyMonster)
                            {
                                monster.Horver();
                            }
                        }).InternalTransition<MonsterController>(Trigger.Exit, (t) =>
                        {
                            var monster = (MonsterController)t.Parameters[0];
                            if (monster.IsMyMonster && monster != ActiveMonster)
                            {
                                monster.HorverEnd();
                            }
                        })
        ;

        // Battle/PlayerTurn/SelectingTarget
        state_machine.Configure(State.SelectingTarget).SubstateOf(State.PlayerTurn)
        .OnEntry(() =>
        {
            SelectingRange.ForEach(m => m.Active());
        })
        .OnExit(() =>
        {
            SelectingRange.ForEach(m => m.BeNormal());
        })
        .InternalTransition<MonsterController>(Trigger.Click, (t) =>
        {
            var monster = (MonsterController)t.Parameters[0];
            if (SelectingRange.Contains(monster))
            {
                if (IsSkill)
                {
                    SkillProcessInfo.FunctionOnMonsters.Clear();
                    SkillProcessInfo.FunctionOnMonsters.Add(monster);
                    state_machine.Fire(Trigger.SkillProcess);
                }
                else
                {
                    state_machine.Fire(AttackEnemyTrigger, monster);
                }
            }
        })
        .InternalTransition<MonsterController>(Trigger.Enter, (t) =>
        {
            var monster = (MonsterController)t.Parameters[0];
            if (SelectingRange.Contains(monster))
            {
                monster.Horver();
            }
        })
        .InternalTransition<MonsterController>(Trigger.Exit, (t) =>
        {
            var monster = (MonsterController)t.Parameters[0];
            if (SelectingRange.Contains(monster))
            {
                monster.HorverEnd();
            }
        })
        .Permit(Trigger.SkillProcess, State.SkillProcessing)
        .PermitDynamic(AttackEnemyTrigger, (mons) =>
        {
            return State.Attacking;
        })
        .Permit(Trigger.Cancle, State.Idle);

        // Battle / PlayerTurn / Attacking
        state_machine.Configure(State.Attacking).SubstateOf(State.PlayerTurn)
                    .Permit(Trigger.AttackOver, State.Idle)
                    .OnEntryFrom(AttackEnemyTrigger, async (mons) =>
                      {
                          await TalkUI.Instance.Open(ActiveMonster.Data.Info.MonsterName + "攻击!", YuGiAvatarName.Attack);
                          ActiveMonster.MonsterActive = false;
                          ActiveMonster.BeNormal();
                          await ActiveMonster.Attack(mons);
                          ActiveMonster = null;
                          await TalkUI.Instance.Close();
                          state_machine.Fire(Trigger.AttackOver);
                      })
                    ;

        // Battle / PlayerTurn / SkillProcessing
        state_machine.Configure(State.SkillProcessing).SubstateOf(State.PlayerTurn)
            .Permit(Trigger.SkillProcessOver, State.Idle)
            .OnEntry(async () =>
            {
                SkillProcessInfo.ActiveMonster.MonsterActive = false;
                SkillProcessInfo.ActiveMonster.BeNormal();

                await SkillProcessInfo.ActiveMonster.ExcuteSkill(SkillProcessInfo);
                state_machine.Fire(Trigger.SkillProcessOver);
            });

        // Battle/EnemyTurn
        state_machine.Configure(State.EnemyTurn).SubstateOf(State.Battle)
            .OnEntry(async () =>
            {
                await Task.Delay(1000);
                await InterludeUI.Instance.WaitInterlude("敌方回合");
                foreach (var mons in EnemyMonsters)
                {
                    await mons.ActiveStatuses();
                }
                foreach (var enemy in EnemyMonsters)
                {
                    await enemy.Attack(PlayerMonsters.Random());
                }
                state_machine.Fire(Trigger.TurnEnd);
            })
            .Permit(Trigger.TurnEnd, State.PlayerTurn);

        // Win
        state_machine.Configure(State.Win)
                    .OnEntry(async () =>
                    {
                        await InterludeUI.Instance.WaitInterlude("战斗胜利");
                        BattleRecord.Instance.Log("战斗胜利");
                        OnWin?.Invoke();
                    });

        // Lose
        state_machine.Configure(State.Lose)
            .OnEntry(async () =>
            {
                await InterludeUI.Instance.WaitInterlude("战斗失败");
                BattleRecord.Instance.Log("战斗失败");
                OnLose?.Invoke();
            });


        state_machine.OnUnhandledTrigger((s, t) => { });
#if UNITY_EDITOR
        state_machine.OnTransitioned((t) => { Debug.Log("from " + t.Source + " to " + t.Destination + " on " + t.Trigger); Debug.Log(state_machine.ToString()); });
#endif

    }

    private void RegisterEvent()
    {
        foreach (var monster in PlayerMonsters)
        {
            monster.OnClicked.AddListener(() =>
            {
                state_machine.Fire(MonsterClickedTrigger, monster);
            });

            monster.OnMouseEntered.AddListener(() =>
            {
                state_machine.Fire(MouseEnterMonsterTrigger, monster);
            });

            monster.OnMouseExited.AddListener(() =>
            {
                state_machine.Fire(MouseExitMonsterTrigger, monster);
            });

            monster.OnMonsterDie.AddListener(() =>
            {
                state_machine.Fire(MonsterDieTrigger, monster);
            });
        }

        foreach (var monster in EnemyMonsters)
        {
            monster.OnClicked.AddListener(() =>
            {
                state_machine.Fire(MonsterClickedTrigger, monster);
            });

            monster.OnMouseEntered.AddListener(() =>
            {
                state_machine.Fire(MouseEnterMonsterTrigger, monster);
            });

            monster.OnMouseExited.AddListener(() =>
            {
                state_machine.Fire(MouseExitMonsterTrigger, monster);
            });

            monster.OnMonsterDie.AddListener(() =>
            {
                state_machine.Fire(MonsterDieTrigger, monster);
            });
        }

        BattleMenu.OnAttack += () =>
                        state_machine.Fire(Trigger.AttackCommand);
        BattleMenu.OnDefense += () =>
                        state_machine.Fire(Trigger.DefenseCommand);


        BattleMenu.SpecialSkillList.OnSkillTrigger.AddListener((sk) =>
        {

            var spSk = sk as SpecialSkill;
            var skInfo = ActiveMonster.Data.SkillUseInfos.First(inf => inf.TheSkill == spSk);
            switch (spSk.Use)
            {
                case SpecialSkill.UseRange.One:
                    if (skInfo.ThisBattleUsed)
                    {
                        return;
                    }
                    break;
                case SpecialSkill.UseRange.Many:
                    break;
                case SpecialSkill.UseRange.OneTurn:
                    if (skInfo.ThisTurnUsed)
                    {
                        return;
                    }
                    break;
                case SpecialSkill.UseRange.Death:
                    break;
                default:
                    break;
            }
            skInfo.ThisBattleUsed = true;
            skInfo.ThisTurnUsed = true;
            state_machine.Fire(SpecialSkillTrigger, spSk);
        });
        BattleMenu.BattleSkillList.OnSkillTrigger.AddListener((sk) =>
        {

            var btsk = sk as BattleSKill;
            if (ActiveMonster.Data.CurrentBattleSkillPoint >= btsk.Cost)
            {
                state_machine.Fire(BattleSkillTrigger, btsk);
            }
            else
            {
                BattleRecord.Log("战技 " + sk.SkillName + " 并没有足够的战技点去发动!");
            }
        });

        foreach (var mon in PlayerMonsters)
        {
            HUD.Instance.RegisterPlayerMonster(mon);
        }
        foreach (var mon in EnemyMonsters)
        {
            HUD.Instance.RegisterEnemyMonster(mon);
        }
    }

    public void Init(MonsterController.MonsterData[] playerMons, MonsterInfo[] enemyMons)
    {
        for (int i = 0; i < playerMons.Length; i++)
        {
            PlayerMonsterTemplates[i].Init(true, data: playerMons[i]);
            PlayerMonsterTemplates[i].gameObject.SetActive(true);
            PlayerMonsters.Add(PlayerMonsterTemplates[i]);
        }
        for (int i = 0; i < enemyMons.Length; i++)
        {
            EnemyMonsterTemplates[i].Init(false, info: enemyMons[i]);
            EnemyMonsterTemplates[i].gameObject.SetActive(true);
            EnemyMonsters.Add(EnemyMonsterTemplates[i]);
        }
        InitStateMachine();
        RegisterEvent();
        PlayerMonsters.ForEach((Action<MonsterController>)(m => { m.RecoveryBattleSkillPoint(); m.UpdateDataAndState(); if (m.TheMonsterState == MonsterController.MonsterState.Die) { PlayerMonsters.Remove(m); m.gameObject.SetActive(false); } }));
        EnemyMonsters.ForEach((Action<MonsterController>)(m => { m.UpdateDataAndState(); if (m.TheMonsterState == MonsterController.MonsterState.Die) { PlayerMonsters.Remove(m); m.gameObject.SetActive(false); } }));
        state_machine.Fire(Trigger.Start);
    }

    private void CheckState()
    {
        if (PlayerMonsters.Count == 0)
            state_machine.Fire(Trigger.Lose);
        else if (EnemyMonsters.Count == 0)
            state_machine.Fire(Trigger.Win);
    }
    #endregion

    void Update()
    {
        //CheckState();
        if (Input.GetMouseButtonDown(0))
        {
            //Collider2D[] cols = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), MonsterLayer.value);
            //if (cols == null || cols.Length == 0)
            //{
            //    ActiveMonster?.gameObject.SetNormalMaterial();
            //    BattleMenu.gameObject.SetActive(false);
            //}
        }
        else if (Input.GetMouseButtonDown(1))
        {
            state_machine.Fire(Trigger.Cancle);
        }
    }

}