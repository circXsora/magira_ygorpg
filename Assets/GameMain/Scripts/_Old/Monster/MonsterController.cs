using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour, IMonsterHUDDataProvider
{


    public SpriteRenderer SpriteRenderer;
    public TMPro.TMP_Text MonsterNameText;

    public bool MonsterActive = true;
    public bool IsMyMonster = true;

    private DamageNumber DamageNumber;

    #region Status

    public List<MonsterStatus> statuses = new List<MonsterStatus>();
    public async Task ActiveStatuses()
    {
        for (int i = statuses.Count - 1; i >= 0; i--)
        {
            var status = statuses[i];
            if (status.TurnCount == 0)
            {
                statuses.Remove(status);
                continue;
            }
            status.Active(this);
            await Task.Delay(500);
        }

    }

    #endregion

    #region Buff/Debuff

    #endregion

    #region UserInteractFSM

    public enum InteractState
    {
        Parent,

        Normal,
        Active,
        Horver,
        Selecting
    }

    private enum InteractTrigger
    {
        Normal,
        Active,
        Horver,
        HorverEnd,
        Selecting,
        UnSelecting
    }

    public void BeNormal() => UserInteractFSM.Raise(InteractTrigger.Normal);
    public void Active() => UserInteractFSM.Raise(InteractTrigger.Active);
    public void Horver() => UserInteractFSM.Raise(InteractTrigger.Horver);
    public void Selecting() => UserInteractFSM.Raise(InteractTrigger.Selecting);
    public void UnSelecting() => UserInteractFSM.Raise(InteractTrigger.UnSelecting);
    public void HorverEnd() => UserInteractFSM.Raise(InteractTrigger.HorverEnd);

    private Stateless.StateMachine<InteractState, InteractTrigger> UserInteractFSM;

    private void InitFSM()
    {
        UserInteractFSM = new Stateless.StateMachine<InteractState, InteractTrigger>(InteractState.Normal);

        UserInteractFSM.Configure(InteractState.Parent)
            .Permit(InteractTrigger.Normal, InteractState.Normal)
            .Permit(InteractTrigger.Selecting, InteractState.Selecting);

        UserInteractFSM.Configure(InteractState.Normal).SubstateOf(InteractState.Parent)
            .OnEntry(() =>
            {
                CloseOutline();
            })
            .Permit(InteractTrigger.Active, InteractState.Active);

        UserInteractFSM.Configure(InteractState.Active).SubstateOf(InteractState.Parent)
            .OnEntry(() =>
            {
                Color color;
                if (IsMyMonster)
                {
                    ColorUtility.TryParseHtmlString("#00751F", out color);
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#730600", out color);
                }
                OpenOutline(color);
            })
            .Permit(InteractTrigger.Horver, InteractState.Horver);

        UserInteractFSM.Configure(InteractState.Horver).SubstateOf(InteractState.Parent)
            .OnEntry(() =>
            {
                Color color;
                if (IsMyMonster)
                {
                    ColorUtility.TryParseHtmlString("#00FF44", out color);
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#FF0E00", out color);
                }
                OpenOutline(color);
            })
            .Permit(InteractTrigger.HorverEnd, InteractState.Active);

        UserInteractFSM.Configure(InteractState.Selecting).SubstateOf(InteractState.Parent)
            .OnEntry(() =>
            {
                Color color;
                if (IsMyMonster)
                {
                    ColorUtility.TryParseHtmlString("#00FF44", out color);
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#FF0E00", out color);
                }
                OpenOutline(color);
            })
            .Permit(InteractTrigger.UnSelecting, InteractState.Active);

        UserInteractFSM.OnUnhandledTrigger((s, t) => { });
    }
    #endregion

    private void OpenOutline(Color color)
    {
        gameObject.SetMaterial("Outline", color);
    }

    private void CloseOutline()
    {
        gameObject.SetNormalMaterial();
    }

    public enum MonsterState
    {
        Idle,
        Defense,
        Die,
    }

    public MonsterState TheMonsterState = MonsterState.Idle;

    [Serializable]
    public class MonsterData
    {
        // 特殊能力使用情况
        public class SkillUseInfo
        {
            public Skill TheSkill;
            public bool ThisTurnUsed = false;
            public bool ThisBattleUsed = false;
        }

        public SkillUseInfo[] SkillUseInfos;

        public MonsterInfo Info;

        public int CurrentBattleSkillPoint = 0;
        public int MaxBattleSkillPoint => Info.LevelDatas[CurrentLevel - 1].MaxBattleSkillPoint;

        public int CurrentLevel = 1;
        public float CurrentHealth, CurrentAttack, CurrentDefense;

        public MonsterData(MonsterInfo info)
        {
            Info = info;
            CurrentHealth = info.LevelDatas[CurrentLevel - 1].MaxHealth;
            CurrentBattleSkillPoint = info.LevelDatas[CurrentLevel - 1].MaxBattleSkillPoint;
            CurrentAttack = info.LevelDatas[CurrentLevel - 1].AttackValue;
            CurrentDefense = info.LevelDatas[CurrentLevel - 1].DefenseValue;

            SkillUseInfos = new SkillUseInfo[SpecialSkills.Length];

            for (int i = 0; i < SpecialSkills.Length; i++)
            {
                SkillUseInfos[i] = new SkillUseInfo();
                SkillUseInfos[i].TheSkill = SpecialSkills[i];
            }

        }

        public Skill[] SpecialSkills
        {
            get
            {
                List<Skill> skills = new List<Skill>();
                for (int i = 0; i < CurrentLevel; i++)
                {
                    skills.AddRange(Info.LevelDatas[i].SpecialSkills);
                }
                return skills.ToArray();
            }
        }
        public Skill[] BattleSkill
        {
            get
            {
                List<Skill> skills = new List<Skill>();
                for (int i = 0; i < CurrentLevel; i++)
                {
                    skills.AddRange(Info.LevelDatas[i].BattleSkills);
                }
                return skills.ToArray();
            }
        }

        public float MaxHealth { get => Info.LevelDatas[CurrentLevel - 1].MaxHealth; }
    }

    public MonsterData Data;
    public event Action<MonsterHUD.HUDViewModel> OnDataChanged;
    public MonsterHUD.HUDViewModel ViewModel
    {
        get
        {
            return new MonsterHUD.HUDViewModel()
            {
                Avatar = Data.Info.Sprite,
                Name = Data.Info.MonsterName,
                HealthBarViewModel = new HealthBar.HealthBarViewModel()
                {
                    Health = Data.CurrentHealth,
                    MaxHealth = Data.MaxHealth
                },
                BattleSkillPanelViewModel = new BattleSkillPanel.BattleSkillPanelViewModel()
                {
                    MaxBattleSkillPoint = Data.MaxBattleSkillPoint,
                    CurrentBattleSkillPoint = Data.CurrentBattleSkillPoint
                }

            };
        }
    }
    public async void UpdateDataAndState()
    {
        OnDataChanged?.Invoke(ViewModel);

        if (Data.CurrentHealth == 0)
        {
            await Die();
        }
    }

    public void Init(bool IsMyMonster, MonsterData data = null, MonsterInfo info = null)
    {
        if (data == null)
        {
            Data = new MonsterData(info);
        }
        else
        {
            Data = data;
        }

        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Data.Info.Sprite;
        DamageNumber = GetComponentInChildren<DamageNumber>(true);
        MonsterNameText.text = Data.Info.MonsterName;
        this.IsMyMonster = IsMyMonster;
        InitFSM();
    }

    public float GoToEnemyTime = 1f, AttackTime = 1f, BackTime = 1f;
    public float FromEnemyDistance = 1.3f;

    public async Task MoveToTargetThen(Action action, MonsterController target)
    {
        var originPos = transform.position;
        transform.DOMove(target.transform.position - new Vector3(transform.localScale.x * FromEnemyDistance, 0), GoToEnemyTime);
        await Task.Delay((int)(GoToEnemyTime * 1000));

        BattleRecord.Instance.Log(Data.Info.MonsterName + " 对 " + target.Data.Info.MonsterName + " 发动攻击 ");
        transform.DOShakePosition(AttackTime);
        await Task.Delay((int)(AttackTime * 1000));

        action?.Invoke();

        transform.DOMove(originPos, BackTime);
        await Task.Delay((int)(BackTime * 1000));
    }

    public async Task Attack(MonsterController target)
    {
        await MoveToTargetThen(() => target.TakeDamage(Data.CurrentAttack), target);
    }

    public async Task ExcuteSkill(BattleInfo.SkillProcessInfo skillProcessInfo)
    {
        Skill skill = skillProcessInfo.Skill;
        if (skill is BattleSKill)
        {
            Data.CurrentBattleSkillPoint -= (skill as BattleSKill).Cost;
            UpdateDataAndState();
        }
        foreach (var target in skillProcessInfo.FunctionOnMonsters)
        {
            switch (skill.Type)
            {
                case Skill.SkillType.Direct:
                    switch (skill.Effect)
                    {
                        case Skill.SkillEffect.Heal:
                            await PlayAnimation("Heal");
                            target.Heal(skill.SkillValues[0]);
                            break;
                        case Skill.SkillEffect.DamageFactor:
                            await MoveToTargetThen(() => target.TakeDamage(Data.CurrentAttack * skill.SkillValues[0]), target);
                            break;
                        case Skill.SkillEffect.DamageNumbers:
                            await MoveToTargetThen(() => target.TakeDamage(skill.SkillValues[0]), target);
                            break;
                        case Skill.SkillEffect.BeDamagedToZero:
                            break;
                        default:
                            break;
                    }
                    break;
                case Skill.SkillType.Status:
                    switch (skill.Effect)
                    {
                        case Skill.SkillEffect.Heal:
                            await PlayAnimation("HealStatus");
                            target.statuses.Add(new HealStatus(skill.SkillValues[0], skill.SkillLastTime));
                            break;
                        case Skill.SkillEffect.DamageFactor:
                            await MoveToTargetThen(() => target.statuses.Add(new DamageStatus(Data.CurrentAttack * skill.SkillValues[0], skill.SkillLastTime)), target);
                            break;
                        case Skill.SkillEffect.DamageNumbers:
                            await MoveToTargetThen(() => target.statuses.Add(new DamageStatus(skill.SkillValues[0], skill.SkillLastTime)), target);
                            break;
                        case Skill.SkillEffect.BeDamagedToZero:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void RecoveryBattleSkillPoint()
    {
        Data.CurrentBattleSkillPoint = Data.MaxBattleSkillPoint;
        UpdateDataAndState();
    }

    public async void TakeDamage(float value)
    {
        value = Math.Max(0, value - Data.CurrentDefense);
        Data.CurrentHealth = Mathf.Max(0, Data.CurrentHealth - value);
        SoundsManager.Instance.PlaySE(SEName.GetDamage);
        transform.DOPunchPosition(new Vector3(transform.localScale.x * .1f, 0, 0), AttackTime);
        DamageNumber.PlayDamageNubmer(value);
        BattleRecord.Instance.Log(Data.Info.MonsterName + " 受到 " + value + " 伤害");
        await Task.Delay((int)(1000f *AttackTime));
        UpdateDataAndState();
    }

    public async Task PlayAnimation(string animationName)
    {
        switch (animationName)
        {
            case "Die":
                gameObject.SetMaterial("Dissolve");
                gameObject.GetComponent<Renderer>().material.DOFloat(0, "_Fade", 2);
                await Task.Delay(2000);
                break;
            default:
                await Task.Delay(1000);
                break;
        }
    }

    public void Heal(float value)
    {
        DamageNumber.PlayHealNubmer(value);
        value = Mathf.Min(Data.MaxHealth, value + Data.CurrentHealth);
        SoundsManager.Instance.PlaySE(SEName.HealthUp);
        Data.CurrentHealth = value;
        UpdateDataAndState();
    }

    private async Task Die()
    {
        SoundsManager.Instance.PlaySE(SEName.Die);
        await PlayAnimation("Die");
        TheMonsterState = MonsterState.Die;
        OnMonsterDie?.Invoke();
    }

    public void Defense()
    {
        TheMonsterState = MonsterState.Defense;
        BattleRecord.Instance.Log(Data.Info.MonsterName + " 变为守备表示, 守备指为" + Data.CurrentDefense);
    }


    #region Unity Event
    public UnityEvent OnClicked, OnMouseEntered, OnMouseOvered, OnMouseExited, OnMonsterDie;

    private void OnMouseEnter()
    {
        OnMouseEntered?.Invoke();
    }

    private void OnMouseOver()
    {
        OnMouseOvered?.Invoke();
    }

    private void OnMouseExit()
    {
        OnMouseExited?.Invoke();
    }

    private void OnMouseDown()
    {
        OnClicked?.Invoke();
    }
    #endregion

}
