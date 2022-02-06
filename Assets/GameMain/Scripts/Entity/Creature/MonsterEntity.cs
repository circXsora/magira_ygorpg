using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class MonsterEntity : CreatureEntity
    {
        public MonsterLogic MonsterLogic => Logic as MonsterLogic;
    }
}