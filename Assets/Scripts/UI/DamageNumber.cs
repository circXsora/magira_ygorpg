using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class DamageNumber : MonoBehaviour
{
    public float AnimationTime = 1f;
    public float UpHeight = 40;
    public Vector3 initial_pos;
    TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        initial_pos = transform.localPosition;
        text = GetComponent<TMPro.TMP_Text>();
    }

    public async void PlayDamageNubmer(float damagemVal)
    {
        text.color = Color.red;
        await PlayNumber("-" + damagemVal);
    }

    public async void PlayHealNubmer(float healval)
    {
        text.color = Color.green;
        await PlayNumber("+" + healval);
    }

    private async Task PlayNumber(string val)
    {
        transform.localPosition = initial_pos;
        text.text = val;
        text.enabled = true;
        transform.DOLocalMoveY(transform.localPosition.y + UpHeight, AnimationTime);
        await Task.Delay((int)(AnimationTime * 1000));
        text.enabled = false;
    }
}
