//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace BBYGO
{

    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {

        public static SoraProcedureComponent Procedure { get; private set; }
        public static EventComponent Event { get; private set; }
        public static SoraUIComponent UI { get; private set; }
        public static SoraResourceComponent Resource { get; private set; }
        private void Awake()
        {
            Procedure = GetComponentInChildren<SoraProcedureComponent>();
            Event = GetComponentInChildren<EventComponent>();
            UI = GetComponentInChildren<SoraUIComponent>();
            Resource = GetComponentInChildren<SoraResourceComponent>();
        }
    }
}
