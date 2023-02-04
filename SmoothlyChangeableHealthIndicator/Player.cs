using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHeal(int heal)
    {
        int healedHealth = _currentHealth + heal;

        if (healedHealth > _health)
        {
            _currentHealth = _health;
        }
        else
        {
            _currentHealth = healedHealth;
        }

        HealthChanged?.Invoke(_currentHealth, _health);
    }
}
