using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardDisplay : MonoBehaviour
{
    public Sprite Hard1, Hard2, Hard3;
    private SpriteRenderer ren;
    public void SetHard(int hard)
    {
        if (ren == null)
        {
            ren = GetComponent<SpriteRenderer>();
        }
        ren.enabled = true;
        switch (hard)
        {
            case 0:
                ren.enabled = false;
                break;
            case 1:
                ren.sprite = Hard1;
                break;
            case 2:
                ren.sprite = Hard2;
                break;
            case 3:
                ren.sprite = Hard3;
                break;
            default:
                break;
        }
    }
}
