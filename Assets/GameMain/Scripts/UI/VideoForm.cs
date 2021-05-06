//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityGameFramework.Runtime;
using DG.Tweening;
namespace BBYGO
{
    public class VideoForm : UGuiForm
    {
        private bool firstClick = true;
        private float firstClickComputeTime = 0;

        public VideoPlayer videoPlayer;
        public RawImage rawImage;
        public TMPro.TMP_Text TxtLabel;
        Sequence seq;
        public void OnBackgroundButtonClick()
        {
            if (firstClick)
            {
                firstClick = false;
                TxtLabel.gameObject.SetActive(true);
                if (seq != null)
                    seq.Kill();
                seq = DOTween.Sequence();
                seq.Append(TxtLabel.DOFade(0.0f, 1f).From(1.0f));
                seq.Append(TxtLabel.DOFade(1.0f, 1f).From(0.0f));
                seq.SetLoops(-1);
            }
            else
            {

                GameEntry.Event.Raise(this, OPPlayFinishEventArgs.Create());
            }
        }
#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            videoPlayer.targetTexture.Release();
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            // 换个音乐
            GameEntry.Sound.StopMusic();
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (firstClick == false)
            {
                firstClickComputeTime += elapseSeconds;
                if (firstClickComputeTime > 3f)
                {
                    firstClick = true;
                    seq.Kill();
                    TxtLabel.gameObject.SetActive(false);
                    firstClickComputeTime = 0;
                }
            }
        }
    }
}
