//------------------------------------------------------------------------------
//  <copyright file="NumberTextEffect.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/24 17:35:46
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class NumberTextEffect : MonoBehaviour
    {
        public TMPro.TMP_Text text;
        public Vector3 moveParam;
        public float moveTime;
        public void SetNumber(float number)
        {
            text.text = "-" + number.ToString();
        }

        public Task Run()
        {
            text.DOFade(0, moveTime).SetEase(Ease.InQuart);
            return transform.DOMove(transform.position + moveParam, moveTime).AsyncWaitForCompletion();
        }
    }
}