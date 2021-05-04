using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class TalkUI : MGO.SingletonInScene<TalkUI>
{
    [SerializeField]
    private GameObject TalkUIPanel;

    [SerializeField]
    private Image Avatar;

    [SerializeField]
    private TMPro.TMP_Text TalkText;

    public AvatarConfig AvatarConfig;

    public float OpenFadeTime = .4f, CloseFadeTime = .1f;

    public async Task Open(string kotoba, YuGiAvatarName? avatarName = null)
    {
        TalkText.text = kotoba;
        if (avatarName != null)
        {
            // Change Avatar
            Avatar.preserveAspect = true;
            var sprite = AvatarConfig.Avatars[avatarName.Value];
            var radio = sprite.rect.width / sprite.rect.height;
            var height = Avatar.rectTransform.sizeDelta.y;
            var width = height * radio;
            Avatar.sprite = sprite;
            Avatar.rectTransform.sizeDelta = new Vector2(width, height);
        }
        var group = TalkUIPanel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        TalkUIPanel.SetActive(true);
        DOTween.To(() => group.alpha, val => group.alpha = val, 1, OpenFadeTime);
        await Task.Delay((int)(OpenFadeTime * 1000));
    }
    public async Task Close()
    {
        var group = TalkUIPanel.GetComponent<CanvasGroup>();
        //group.alpha = 1;
        TalkUIPanel.SetActive(true);
        var t = DOTween.To(() => group.alpha, val => group.alpha = val, 0, OpenFadeTime);
        while (t.IsComplete())
        {
            await Task.Yield();
        }

        //await Task.Delay(OpenFadeTime.ToMill());
        TalkUIPanel.SetActive(false);
    }
}
