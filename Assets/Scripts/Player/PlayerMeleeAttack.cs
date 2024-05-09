using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private int _meleeAttackDamage;
    [SerializeField] private float _meleeAttackRadius;
    [SerializeField] private float _meleeAttackDelay;

    private PlayerInput _playerInput;
    private bool _isMeleeAttacking;

    public event Action Attacked;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _meleeAttackRadius);
    }

    private void Attack()
    {
        if (_playerInput.IsMeleeAttackKeyPressed() && _isMeleeAttacking == false)
        {
            StartCoroutine(MeleeAttack());
        }
    }

    private IEnumerator MeleeAttack()
    {
        _isMeleeAttacking = true;

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _meleeAttackRadius);

        foreach (Collider2D target in targets)
        {
            if (target != null && target.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_meleeAttackDamage);
            }
        }

        Attacked?.Invoke();

        yield return new WaitForSeconds(_meleeAttackDelay);

        _isMeleeAttacking = false;
    }
}