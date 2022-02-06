using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace BBYGO
{
    public class SoraEventComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        [FormerlySerializedAs("OnViewPointerEnter")]
        public EventSO OnEntityPointerEnter;
        [FormerlySerializedAs("OnViewPointerExit")]
        public EventSO OnEntityPointerExit;
        [FormerlySerializedAs("OnViewPointerClick")]
        public EventSO OnEntityPointerClick;
    }
}