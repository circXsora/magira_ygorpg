using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public sealed class ActiveSceneChangedEventArgs : GameEventArgs

namespace BBYGO
{

    public sealed class OPPlayFinishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(OPPlayFinishEventArgs).GetHashCode();


        public OPPlayFinishEventArgs()
        {

        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public override void Clear()
        {
           
        }
    }

}