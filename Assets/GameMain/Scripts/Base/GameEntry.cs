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

        public ProcedureComponent Procedure { get; private set; }

        private void Start()
        {
            Procedure = GetComponentInChildren<ProcedureComponent>();
        }
    }
}
