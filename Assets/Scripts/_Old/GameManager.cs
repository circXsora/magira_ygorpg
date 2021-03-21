using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

public class GameManager : Magia.Singleton<GameManager>
{

    enum State
    {
        Empty,
        Free,
        Battle,
    }

    enum Trigger
    {
        GameStart,
        BattleStart,
        BattleEnd,
        Replay,

    }

    public RoomController CurrentRoomCtrl, BossRoomCtrl;

    StateMachine<State, Trigger> machine;

    public MonsterInfo[] PlayerInitalMonsterInfos;

    private bool win = true;
    private void InitMachine()
    {
        machine = new StateMachine<State, Trigger>(State.Empty, FiringMode.Queued);
        machine.Configure(State.Empty)
            .OnEntry(() => Debug.Log("Start"))
            .Permit(Trigger.GameStart, State.Free);

        machine.Configure(State.Free)
            .OnEntry(() => { SoundsManager.Instance.PlayBGM(BGMName.Free); })
            .OnEntryFrom(Trigger.GameStart, async () =>
             {
                 var op = SceneManager.LoadSceneAsync("Free");
                 while (!op.isDone)
                 {
                     await Task.Delay(25);
                 }
                 PlayerController.Instance.Datas = new MonsterController.MonsterData[PlayerInitalMonsterInfos.Length];
                 for (int i = 0; i < PlayerInitalMonsterInfos.Length; i++)
                 {
                     PlayerController.Instance.Datas[i] = new MonsterController.MonsterData(PlayerInitalMonsterInfos[i]);
                 }
             })
            .OnEntryFrom(Trigger.BattleEnd, async () =>
            {
                if (win)
                {
                    CurrentRoomCtrl.BattleTrigger.gameObject.SetActive(false);
                    CurrentRoomCtrl.Monsters = new MonsterInfo[] { };
                    BossRoomCtrl = RoomGenerator.Instance.EndRoom.Room.GetComponent<RoomController>();
                    if (CurrentRoomCtrl == BossRoomCtrl)
                    {
                        var op = SceneManager.LoadSceneAsync("Success");
                        while (!op.isDone)
                        {
                            await Task.Delay(25);
                        }
                        SuccessManager.Instance.OnNextStage.AddListener(() => machine.Fire(Trigger.GameStart));
                        return;
                    }
                    RoomGenerator.Instance.ComputeRoomHard();

                    foreach (var door in CurrentRoomCtrl.Doors)
                    {
                        door.GetComponent<Collider2D>().enabled = false;
                    }
                    FreeSceneManager.Instance.gameObject.SetActive(true);
                }
                else
                {
                    FreeSceneManager.Instance.gameObject.SetActive(false);
                    var op = SceneManager.LoadSceneAsync("Faild");
                    while (!op.isDone)
                    {
                        await Task.Delay(25);
                    }
                    FaildManager.Instance.OnReplay.AddListener(() => machine.Fire(Trigger.GameStart));
                }
            })
            .PermitReentry(Trigger.GameStart)
            .Permit(Trigger.BattleStart, State.Battle);

        machine.Configure(State.Battle)
            .OnEntry(async () =>
            {
                var op = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);
                SoundsManager.Instance.PlayBGM(BGMName.Battle);
                op.allowSceneActivation = false;
                while (!op.isDone)
                {
                    await Task.Delay(25);
                    if (op.progress >= .9f)
                    {
                        FreeSceneManager.Instance.gameObject.SetActive(false);
                        op.allowSceneActivation = true;
                    }
                }
                
                var objs = SceneManager.GetSceneByName("Battle").GetRootGameObjects();
                var battleSys = BattaleSystem.Instance;
                battleSys.OnWin.AddListener(ExitBattle);
                battleSys.OnWin.AddListener(() => win = true);
                battleSys.OnLose.AddListener(ExitBattle);
                battleSys.OnLose.AddListener(() => win = false);
                battleSys.Init(PlayerController.Instance.Datas, CurrentRoomCtrl.Monsters);
            })
            .Permit(Trigger.BattleEnd, State.Free);

        machine.OnUnhandledTrigger((s, t) => { });
        machine.Fire(Trigger.GameStart);
    }

    //private async void Win() { }
    //private async void Lose() { }

    private async void ExitBattle()
    {
        var op = SceneManager.UnloadSceneAsync("Battle");
        while (!op.isDone)
        {
            await Task.Delay(25);
        }

        machine.Fire(Trigger.BattleEnd);
    }

    internal void Battle(RoomController roomController)
    {
        CurrentRoomCtrl = roomController;
        machine.Fire(Trigger.BattleStart);
        CurrentRoomCtrl.Battled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMachine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( KeyCode.R))
        {
            machine.Fire(Trigger.GameStart);
        }
    }
}
