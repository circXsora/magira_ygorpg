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
        private MaterialComponent.MaterialChanger materialChanger;
        [RequiredField]
        public MaterialComponent.MaterialType materialType;
        protected override string info
        {
            get { return string.Format("Change To {0} Material", materialType.ToString()); }
        }

        protected override string OnInit()
        {
            materialChanger = blackboard.GetVariable<MaterialComponent.MaterialChanger>(nameof(MaterialComponent.MaterialChanger)).value;
            return base.OnInit();
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            materialChanger.ChangeTo(materialType);
        }
    }
}