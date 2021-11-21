//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BBYGO
{
    public class AttackData
    {
        public CreatureOldLogic Player { get; private set; }
        public List<CreatureOldLogic> Targets { get; private set; }
        public int AttackValue { get; private set; }
    }
}
