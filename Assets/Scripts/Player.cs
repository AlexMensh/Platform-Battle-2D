using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }

    public void RestoreHealth(int healAmount)
    {
        if (_health < _maxHealth)
        {
            _health += healAmount;

            if (_health > _maxHealth)
                _health = _maxHealth;
        }
    }
}