//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using MGO;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{

    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static EventComponent Event { get; private set; }
        public static UIComponent UI { get; private set; }
        public static ResourceComponent Resource { get; private set; }
        public static EnvironmentComponent Environment { get; private set; }
        public static CreaturesComponent Creatures { get; private set; }
        public static ConfigComponent Config { get; private set; }
        public static MaterialComponent Material { get; private set; }
        public static FSMComponent FSM { get; private set; }
        public static ContextComponent Context { get; private set; }
        public static AIComponent AI { get; private set; }
        public static VisualEffectComponent VisualEffect { get; private set; }
        public static ProcedureComponent Procedure { get; private set; }
        public static GameModuleComponent GameModule { get; private set; }
        public static WorkComponent Work { get; private set; }
        public static CardComponent Card { get; private set; }
        public static InputComponent Input { get; private set; }
        public static Camera MainCamera;

        private void Awake()
        {
            TaskScheduler.UnobservedTaskException += (s, e) => Debug.LogException(e.Exception);

            Procedure = GetComponentInChildren<ProcedureComponent>();
            GameModule = GetComponentInChildren<GameModuleComponent>();
            Event = GetComponentInChildren<EventComponent>();
            UI = GetComponentInChildren<UIComponent>();
            Resource = GetComponentInChildren<ResourceComponent>();
            Environment = GetComponentInChildren<EnvironmentComponent>();
            Creatures = GetComponentInChildren<CreaturesComponent>();
            Config = GetComponentInChildren<ConfigComponent>();
            MainCamera = GetComponentInChildren<Camera>();
            Material = GetComponentInChildren<MaterialComponent>();
            FSM = GetComponentInChildren<FSMComponent>();
            AI = GetComponentInChildren<AIComponent>();
            Context = GetComponentInChildren<ContextComponent>();
            VisualEffect = GetComponentInChildren<VisualEffectComponent>();
            Work = GetComponentInChildren<WorkComponent>();
            Card = GetComponentInChildren<CardComponent>();
            Input = GetComponentInChildren<InputComponent>();
        }
    }
}
