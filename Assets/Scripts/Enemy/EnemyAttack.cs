using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackSpeed;

    private float _attackTime—oefficient = 1f;
    private float _startAttackTime;

    public event Action Attacked;

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

    private void Attack()
    {
        if (Time.time >= _startAttackTime)
        {
            ApplyDamage();
            _startAttackTime = Time.time + _attackTime—oefficient / _attackSpeed;
        }
    }

    private void ApplyDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out Player player))
            {
                player.TakeDamage(_attackPower);
                Attacked?.Invoke();
            }
        }
    }
}