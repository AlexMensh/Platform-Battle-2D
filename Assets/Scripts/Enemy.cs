using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if ( _health <= 0)
            Destroy(gameObject);
    }
}