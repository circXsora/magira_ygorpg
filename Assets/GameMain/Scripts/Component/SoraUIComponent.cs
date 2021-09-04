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

        public async Task<SoraUIForm> Load(UIType ui)
        {
            var uiPrefab = await GameEntry.Resource.LoadAsync<GameObject>("UI/UIForms/" + ui.ToString());
            var uiInstance = Instantiate(uiPrefab);
            uiFormsDic.Add(ui.ToString(), uiInstance);
            uiInstance.transform.SetParent(UIParent);
            return uiInstance.GetComponent<SoraUIForm>();
        }

        public async Task Open(UIType ui)
        {
            var uiForm = await Load(ui);
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

        internal async Task HideBattleCommandMenu()
        {
            var key = UIType.MonsterCommandMenuForm.ToString();
            if (uiFormsDic.TryGetValue(key, out var formObj))
            {
                var form = formObj.GetComponent<MonsterCommandMenuForm>();
                await form.Hide();
            }
        }

        public async Task SetBattleCommandMenuTo(CreatureView view)
        {
            var key = UIType.MonsterCommandMenuForm.ToString();
            if (!uiFormsDic.TryGetValue(key, out var formObj))
            {
                await Load(UIType.MonsterCommandMenuForm);
                formObj = uiFormsDic[key];
            }
            var form = formObj.GetComponent<MonsterCommandMenuForm>();
            form.SetToView(view);
            await form.Show();
        }
    }
}