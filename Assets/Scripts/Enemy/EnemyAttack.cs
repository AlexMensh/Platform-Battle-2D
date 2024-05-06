using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackSpeed;

    private float _startAttackTime;

    public event Action Attacked;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            Attack(player);
        }
    }

    private void Attack(Player player)
    {
        if (Time.time >= _startAttackTime)
        {
            player.TakeDamage(_attackPower);
            Attacked?.Invoke();
            _startAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}