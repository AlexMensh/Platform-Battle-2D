using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackSpeed;

    private float _startAttackTime;

    public Action OnAttacked;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }

    private void Attack(Player player)
    {
        if (Time.time >= _startAttackTime)
        {
            player.TakeDamage(_attackPower);
            OnAttacked?.Invoke();
            _startAttackTime = Time.time + 1f / _attackSpeed;
        }
    }
}