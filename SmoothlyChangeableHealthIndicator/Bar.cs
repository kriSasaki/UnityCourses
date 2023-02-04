using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] private float _changingSpeed;

    private Coroutine _sliderChanger;

    public void OnValueChanged(int value, int maxValue)
    {
        float targetValue = (float)value / maxValue;

        if (_sliderChanger != null)
        {
            StopCoroutine(_sliderChanger);
            _sliderChanger = StartCoroutine(ChangeSlider(targetValue));
        }
        else
        {
            _sliderChanger = StartCoroutine(ChangeSlider(targetValue));
        }
    }

    private IEnumerator ChangeSlider(float targetValue)
    {
        while (Slider.value != targetValue)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, targetValue, _changingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
