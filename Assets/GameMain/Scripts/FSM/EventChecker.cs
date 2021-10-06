using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class EventChecker : ConditionTask
    {

        [RequiredField]
        public BBParameter<EventSO> @event;

        protected override string info
        {
            get { return string.Format("Event {0} Raised", @event.ToString()); }
        }

        protected override string OnInit()
        {
            @event.value.AddListener(OnRaise);
            return null;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void OnRaise(object sender, object e)
        {
            YieldReturn(true);
        }

        protected override bool OnCheck() { return false; }
    }
}