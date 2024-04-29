using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }

    public void RestoreHealth(int healAmount)
    {
        if (_health <= _maxHealth)
            _health += healAmount;
        else
            _health = _maxHealth;
    }
}
