using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly Dictionary<string, GameObject> uiFormsDic = new Dictionary<string, GameObject>();
        public async Task Open(UIType ui)
        {
            var uiPrefab = await GameEntry.Resource.LoadAsync<GameObject>("UI/UIForms/" + ui.ToString());
            var uiInstance = Instantiate(uiPrefab);
            uiFormsDic.Add(ui.ToString(), uiInstance);
            uiInstance.transform.SetParent(UIParent);
            var uiForm = uiInstance.GetComponent<SoraUIForm>();
            await uiForm.Show();
        }

        public async Task Close(UIType ui)
        {
            if (uiFormsDic.TryGetValue(ui.ToString(), out var uiInstance))
            {
                uiFormsDic.Remove(ui.ToString());
                var uiForm = uiInstance.GetComponent<SoraUIForm>();
                await uiForm.Hide();
                Destroy(uiInstance);
            }
        }
    }
}