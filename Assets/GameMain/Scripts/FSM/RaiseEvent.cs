using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class RaiseEvent<T> : ActionTask
    {
        [RequiredField]
        public BBParameter<EventSO> @event;

        [RequiredField]
        public BBParameter<T> data;

        protected override string info
        {
            get { return string.Format("Raise Event {0}", @event.ToString()); }
        }
        protected override void OnExecute()
        {
            @event?.value.Raise(this, data);
        }
    }
}