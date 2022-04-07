//------------------------------------------------------------------------------
//  <copyright file="CreatureEneity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:45:09
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public abstract class CreatureView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MaterialChanger changer;
        public MaterialChanger MaterialChanger => changer;
        public CreatureVisualEffectConfigSO visualEffectConfig;
        public CreatureInfo Info { get; set; }

        public bool Selectable { get; set; } = false;
        protected virtual void Awake()
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Selectable)
            {
                //GameEntry.Event.OnEntityPointerClick.Raise(this, eventData);
            }
        }

        public async Task PerformVisualEffect(VisualEffectTypeSO visualEffectType, TimePointHandler[] timePointCallbacks = null)
        {
            if (visualEffectConfig == null)
            {
                visualEffectConfig = GameEntry.Creatures.GetCreatureVisualEffectConfig(Info.entryId);
            }
            await GameEntry.VisualEffect.Perform(this, visualEffectConfig.effectParams[visualEffectType], timePointCallbacks);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Selectable)
            {
                //GameEntry.Event.OnEntityPointerExit.Raise(this, eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Selectable)
            {
                //GameEntry.Event.OnEntityPointerEnter?.Raise(this, eventData);
            }
        }

        public virtual async Task Show()
        {
            gameObject.SetActive(true);
        }
        public virtual async Task Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual async Task Destroy()
        {
            Destroy(gameObject);
        }
    }
}