using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public TMPro.TMP_Text NumberText;


    public Gradient Gradient;

    public HealthBarViewModel ViewModel
    {
        set
        {
            if (value == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Slider.maxValue = value.MaxHealth;
                PlaySliderAnimation(value);
            }
        }
    }
    public void PlaySliderAnimation(HealthBarViewModel value)
    {
        gameObject.SetActive(true);
        var dt = Slider.DOValue(value.Health, 1);
        dt.OnUpdate(() =>
        {
            //Slider.value = value.Health;
            var color = Gradient.Evaluate(Slider.normalizedValue);
            Slider.fillRect.gameObject.GetComponent<Image>().color = color;
            NumberText.text = string.Format("{0} / {1}", (int)Slider.value, value.MaxHealth);
        });

    }
    public class HealthBarViewModel
    {
        public float Health, MaxHealth;
    }

}
