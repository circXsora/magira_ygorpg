//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Threading.Tasks;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{

    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static SoraEventComponent Event { get; private set; }
        public static SoraUIComponent UI { get; private set; }
        public static SoraResourceComponent Resource { get; private set; }
        public static EnvironmentComponent Environment { get; private set; }
        public static CreaturesComponent Creatures { get; private set; }
        public static ConfigComponent Config { get; private set; }
        public static MaterialComponent Material { get; private set; }
        public static FSMComponent FSM { get; private set; }
        public static ContextComponent Context { get; private set; }
        public static AIComponent AI { get; private set; }
        public static Camera MainCamera;

        private void Awake()
        {
            TaskScheduler.UnobservedTaskException += (s, e) => Debug.LogException(e.Exception);

            Event = GetComponentInChildren<SoraEventComponent>();
            UI = GetComponentInChildren<SoraUIComponent>();
            Resource = GetComponentInChildren<SoraResourceComponent>();
            Environment = GetComponentInChildren<EnvironmentComponent>();
            Creatures = GetComponentInChildren<CreaturesComponent>();
            Config = GetComponentInChildren<ConfigComponent>();
            MainCamera = GetComponentInChildren<Camera>();
            Material = GetComponentInChildren<MaterialComponent>();
            FSM = GetComponentInChildren<FSMComponent>();
            AI = GetComponentInChildren<AIComponent>();
            Context = GetComponentInChildren<ContextComponent>();
        }
    }
}
