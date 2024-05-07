using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _meleeAttackDamage;
    [SerializeField] private float _meleeAttackRadius;
    [SerializeField] private float _meleeAttackSpeed;

    [SerializeField] private int _vampiricAttackDamage;
    [SerializeField] private float _vampiricAttackRadius;
    [SerializeField] private float _vampiricAttackSpeed;
    [SerializeField] private float _vampiricAttackDuration;

    private Health _health;
    private float _attackTime—oefficient = 1f;
    private float _startAttackTime;
    private float _vampiricAttackTimer;

    public event Action Attacked;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        MeleeAttack();
        VampiricAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _meleeAttackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _vampiricAttackRadius);
    }

    private void MeleeAttack()
    {
        if (Time.time >= _startAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _meleeAttackRadius);

                foreach (Collider2D target in targets)
                {
                    if (target.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(_meleeAttackDamage);
                    }
                }

                Attacked?.Invoke();

                _startAttackTime = Time.time + _attackTime—oefficient / _meleeAttackSpeed;
            }
        }
    }

    private void VampiricAttack()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            _vampiricAttackTimer = _vampiricAttackDuration;
        }

        if (_vampiricAttackTimer > 0)
        {
            _vampiricAttackTimer -= Time.deltaTime;

            if (Time.time >= _startAttackTime)
            {
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _vampiricAttackRadius);

                foreach (Collider2D target in targets)
                {
                    if (target.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(_vampiricAttackDamage);

                        _health.Heal(_vampiricAttackDamage);
                        _startAttackTime = Time.time + _attackTime—oefficient / _vampiricAttackSpeed;
                    }
                }
            }
        }
    }
}