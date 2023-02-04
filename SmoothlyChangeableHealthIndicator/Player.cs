using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction<int, int> HealthChanged;
    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        int healedHealth = _currentHealth + heal;

        _currentHealth = Mathf.Clamp(healedHealth, _currentHealth, _health);

        HealthChanged?.Invoke(_currentHealth, _health);
    }
}
