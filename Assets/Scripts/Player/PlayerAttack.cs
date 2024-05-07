using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackSpeed;

    private float _attackTime—oefficient = 1f;
    private float _startAttackTime;

    public event Action Attacked;

    public void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

    public void ApplyDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _attackRadius);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_attackPower);
                Attacked?.Invoke();
            }
        }
    }

    private void Attack()
    {
        if (Time.time >= _startAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ApplyDamage();
                _startAttackTime = Time.time + _attackTime—oefficient / _attackSpeed;
            }
        }
    }
}