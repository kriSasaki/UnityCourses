using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;

    private int _maxSliderValue = 1;

    private void OnEnable()
    {
        _player.HealthChanged += OnValueChanged;
        Slider.value  = _maxSliderValue;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
    }
}
