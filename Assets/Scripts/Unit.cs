using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;

    public event Action<float, float> HealthChanged;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);

        HealthChanged?.Invoke(_health, _maxHealth);
    }

    public void Heal(int healAmount)
    {
        _health = Mathf.Clamp(_health + healAmount, _minHealth, _maxHealth);

        HealthChanged?.Invoke(_health, _maxHealth);
    }
}