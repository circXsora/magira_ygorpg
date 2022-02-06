using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class ChangeMaterial : ActionTask
    {
        private MaterialChanger materialChanger;
        [RequiredField]
        public MaterialType materialType;
        protected override string info
        {
            get { return string.Format("Change To {0} Material", materialType.ToString()); }
        }

        protected override string OnInit()
        {
            materialChanger = blackboard.GetVariable<MaterialChanger>(nameof(MaterialChanger)).value;
            return null;
        }

        protected override void OnExecute()
        {
            materialChanger.ChangeTo(materialType);
            EndAction(true);
        }
    }
}