using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyAttack))]

public class EnemyAnimator : MonoBehaviour
{
    private Animator _enemyAnimator;
    private EnemyAttack _enemyAttack;

    private int _isAttackingHash = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        _enemyAttack.Attacked += AttackPlay;
    }

    private void OnDisable()
    {
        _enemyAttack.Attacked -= AttackPlay;
    }

    private void AttackPlay()
    {
        _enemyAnimator.SetBool(_isAttackingHash, true);
    }
}