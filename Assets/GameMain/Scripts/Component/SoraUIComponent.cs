using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public enum UIType
    {
        MenuForm
    }

    public class SoraUIComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        public Transform UIParent;

        public void Open(UIType ui)
        {
            var uiPrefab = GameEntry.Resource.Load<GameObject>("UI/UIForms/" + ui.ToString());
            var uiInstance = Instantiate(uiPrefab);
            uiInstance.transform.SetParent(UIParent);
        }

        public void Close(UIType ui)
        {

        }
    }
}