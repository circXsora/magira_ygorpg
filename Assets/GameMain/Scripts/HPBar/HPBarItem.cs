//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BBYGO
{
    public class HPBarItem : MonoBehaviour
    {
        private const float AnimationSeconds = 0.3f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutSeconds = 0.3f;

        [SerializeField]
        private Slider m_HPBar = null;

        private Canvas m_ParentCanvas = null;
        private RectTransform m_CachedTransform = null;
        private CanvasGroup m_CachedCanvasGroup = null;
        private EntityLogic m_Owner = null;
        private int m_OwnerId = 0;

        public EntityLogic Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public void Init (EntityLogic owner, Canvas parentCanvas, float fromHPRatio, float toHPRatio)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            m_ParentCanvas = parentCanvas;

            gameObject.SetActive(true);
            transform.SetParent(parentCanvas.transform);
            transform.localScale = Vector3.one;
            transform.localPosition = new Vector3(0, 100, 0);
            m_CachedTransform.sizeDelta = new Vector2(185, 15);

            m_CachedCanvasGroup.alpha = 1f;
            if (m_Owner != owner || m_OwnerId != owner.Id)
            {
                m_HPBar.value = fromHPRatio;
                m_Owner = owner;
                m_OwnerId = owner.Id;
            }

            //Refresh();
        }

        //public bool Refresh()
        //{
        //    if (m_CachedCanvasGroup.alpha <= 0f)
        //    {
        //        return false;
        //    }

        //    if (m_Owner != null && Owner.Available && Owner.Id == m_OwnerId)
        //    {
        //        Vector3 worldPosition = m_Owner.CachedTransform.position + Vector3.forward;
        //        Vector3 screenPosition = GameEntry.Scene.BattleCamera.WorldToScreenPoint(worldPosition);

        //        Vector2 position;
        //        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_ParentCanvas.transform, screenPosition,
        //            m_ParentCanvas.worldCamera, out position))
        //        {
        //            m_CachedTransform.localPosition = position;
        //        }
        //    }

        //    return true;
        //}

        public void Reset()
        {
            StopAllCoroutines();
            m_CachedCanvasGroup.alpha = 1f;
            m_HPBar.value = 1f;
            m_Owner = null;
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            m_CachedTransform = GetComponent<RectTransform>();
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

            m_CachedCanvasGroup = GetComponent<CanvasGroup>();
            if (m_CachedCanvasGroup == null)
            {
                Log.Error("CanvasGroup is invalid.");
                return;
            }
        }
    }
}
