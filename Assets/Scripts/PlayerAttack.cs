using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAttackZone _playerAttackZone;
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackSpeed;

    private float _startAttackTime;

    public Action OnAttacked;

    public void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_playerAttackZone.transform.position, _attackRadius);
    }

    public void ApplyDamage()
    {
        Collider2D target = Physics2D.OverlapCircle(_playerAttackZone.transform.position, _attackRadius);

        if (target.gameObject.GetComponent<Enemy>())
        {
            target.gameObject.GetComponent<Enemy>().TakeDamage(_attackPower);
        }

        OnAttacked?.Invoke();
    }

    private void Attack()
    {
        if (Time.time >= _startAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ApplyDamage();
                OnAttacked?.Invoke();
                _startAttackTime = Time.time + 1f / _attackSpeed;
            }
        }
    }
}