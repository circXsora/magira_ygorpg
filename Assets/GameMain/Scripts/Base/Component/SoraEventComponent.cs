using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class SoraEventComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        public EventSO OnViewPointerEnter, OnViewPointerExit, OnViewPointerClick;
    }
}