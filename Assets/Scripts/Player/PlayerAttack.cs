using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackSpeed;

    private float _startAttackTime;

    public event Action BeenAttacked;

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
        Collider2D target = Physics2D.OverlapCircle(transform.position, _attackRadius);

        if (target.gameObject.GetComponent<Enemy>() && target != null)
        {
            target.gameObject.GetComponent<Enemy>().TakeDamage(_attackPower);
        }

        BeenAttacked?.Invoke();
    }

    private void Attack()
    {
        if (Time.time >= _startAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ApplyDamage();
                BeenAttacked?.Invoke();
                _startAttackTime = Time.time + 1f / _attackSpeed;
            }
        }
    }
}