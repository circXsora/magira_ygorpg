//------------------------------------------------------------------------------
//  <copyright file="SoraUIForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:11:55
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using DG.Tweening;
using MGO;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public enum BgType
    {
        Bg1,
        Bg2,
        Bg3
    }

    public enum UIState
    {
        Init,
        Showing,
        Showed,
        Hiding,
        Hided,
        Suspending,
        Suspended
    }

    public class UIForm : MonoBehaviour
    {
        public float ShowAnimationTime = 0.8f;
        public float HideAnimationTime = 0.8f;

        public virtual UIState UIState { get; protected set; } = UIState.Hided;

        public virtual void Init()
        {
            UIState = UIState.Init;
        }

        public async Task Show()
        {
            UIState = UIState.Showing;
            OnShowBefore();
            await ShowCore();
            OnShowAfter();
            UIState = UIState.Showed;
        }

        public async Task Hide()
        {
            UIState = UIState.Hiding;
            OnHideBefore();
            await HideCore();
            OnHideAfter();
            UIState = UIState.Hided;
        }

        public async Task Suspend()
        {
            UIState = UIState.Suspending;
            OnSuspendBefore();
            await SuspendCore();
            OnSuspendAfter();
            UIState = UIState.Suspended;
        }

        protected virtual void OnSuspendBefore()
        {
            
        }

        protected virtual void OnSuspendAfter()
        {
            
        }

        protected virtual void OnShowBefore()
        {

        }

        protected virtual void OnShowAfter()
        {

        }

        protected virtual void OnHideBefore()
        {

        }

        protected virtual void OnHideAfter()
        {

        }

        protected virtual async Task ShowCore()
        {
            var cg = gameObject.GetOrAddComponent<CanvasGroup>();
            await cg.DOFade(1, ShowAnimationTime).From(0).AsyncWaitForCompletion();
            cg.blocksRaycasts = true;
            cg.interactable = true;
        }

        protected virtual async Task SuspendCore()
        {
            await Task.Yield();
        }

        protected virtual async Task HideCore()
        {
            var cg = gameObject.GetOrAddComponent<CanvasGroup>();
            cg.blocksRaycasts = false;
            cg.interactable = false;
            await cg.DOFade(0, HideAnimationTime).AsyncWaitForCompletion();
        }
    }
}