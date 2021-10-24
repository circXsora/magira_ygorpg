using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public enum UIType
    {
        MenuForm,
        MonsterCommandMenuForm
    }

    public class SoraUIComponent : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        public Transform UIParent;
        public Canvas MainCanvas { get; private set; }
        private readonly Dictionary<string, GameObject> uiFormsDic = new Dictionary<string, GameObject>();

        private void Start()
        {
            MainCanvas = UIParent.GetComponent<Canvas>();
        }

        public async Task<UIForm> Load(UIType ui)
        {
            var uiPrefab = await GameEntry.Resource.LoadAsync<GameObject>("UI/UIForms/" + ui.ToString());
            var uiInstance = Instantiate(uiPrefab);
            uiFormsDic.Add(ui.ToString(), uiInstance);
            uiInstance.transform.SetParent(UIParent);
            return uiInstance.GetComponent<UIForm>();
        }

        public async Task Open(UIType ui)
        {
            UIForm uiForm = null;
            if (uiFormsDic.TryGetValue(ui.ToString(), out var uiInstance))
            {
                uiForm = uiInstance.GetComponent<UIForm>();
            }
            else
            {
                uiForm = await Load(ui);
            }
            await uiForm.Show();
        }

        public async Task Close(UIType ui)
        {
            if (uiFormsDic.TryGetValue(ui.ToString(), out var uiInstance))
            {
                uiFormsDic.Remove(ui.ToString());
                var uiForm = uiInstance.GetComponent<UIForm>();
                await uiForm.Hide();
                Destroy(uiInstance);
            }
        }
    }
}