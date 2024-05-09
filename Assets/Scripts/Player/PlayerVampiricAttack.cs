using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health), typeof(PlayerInput))]
public class PlayerVampiricAttack : MonoBehaviour
{
    [SerializeField] private int _vampiricAttackDamage;
    [SerializeField] private float _vampiricAttackRadius;
    [SerializeField] private float _vampiricAttackDuration;
    [SerializeField] private Slider _vampiricDurationSlider;
    [SerializeField] private VampiricSphere _vampiricSphere;

    private PlayerInput _playerInput;
    private Health _health;
    private bool _isVampiricAttacking;
    private float _timerOneBeatRange = 1f;
    private float _vampiricTimer;

    private void Start()
    {
        _health = GetComponent<Health>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _vampiricAttackRadius);
    }

    private void Attack()
    {
        if (_playerInput.IsVampiricAttackKeyPressed() && _isVampiricAttacking == false)
        {
            StartCoroutine(VampiricAttack());
        }
    }

    private IEnumerator VampiricAttack()
    {
        _isVampiricAttacking = true;

        _vampiricSphere.Activate();

        _vampiricTimer = _vampiricAttackDuration;

        while (_vampiricTimer != 0)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _vampiricAttackRadius);

            foreach (Collider2D target in targets)
            {
                if (target != null && target.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_vampiricAttackDamage);

                    int healingAmount = Mathf.Min(_vampiricAttackDamage, enemy.GetHealthValue());

                    _health.Heal(healingAmount);
                }
            }

            yield return new WaitForSeconds(_timerOneBeatRange);

            _vampiricTimer--;

            _vampiricDurationSlider.value = _vampiricTimer;
        }

        _isVampiricAttacking = false;

        _vampiricSphere.Deactivate();

        _vampiricDurationSlider.value = _vampiricAttackDuration;
    }
}