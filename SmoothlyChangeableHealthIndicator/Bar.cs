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

        _sliderChanger = StartCoroutine(ChangeSlider(targetValue));
    }

    private IEnumerator ChangeSlider(float targetValue)
    {
        if (_sliderChanger != null)
        {
            StopCoroutine(_sliderChanger);
        }

        while (Slider.value != targetValue)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, targetValue, _changingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
