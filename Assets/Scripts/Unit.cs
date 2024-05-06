using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public Action<float, float> OnHealthChanged;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public void RestoreHealth(int healAmount)
    {
        if (_health < _maxHealth)
        {
            _health += healAmount;

            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
}