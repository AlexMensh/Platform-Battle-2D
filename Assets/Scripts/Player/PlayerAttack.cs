using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _meleeAttackDamage;
    [SerializeField] private float _meleeAttackRadius;
    [SerializeField] private float _meleeAttackDelay;

    [SerializeField] private int _vampiricAttackDamage;
    [SerializeField] private float _vampiricAttackRadius;
    [SerializeField] private float _vampiricAttackDuration;

    [SerializeField] private Slider _vampiricDurationSlider;
    [SerializeField] private GameObject _vampiricAuraSprite;

    private Health _health;
    private bool _isMeleeAttacking;
    private bool _isVampiricAttacking;
    private float _timerCoefficient = 1f;
    private float _vampiricTimer;

    public event Action Attacked;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _isMeleeAttacking == false)
        {
            StartCoroutine(MeleeAttack());
        }
        if (Input.GetKeyDown(KeyCode.CapsLock) && _isVampiricAttacking == false)
        {
            StartCoroutine(VampiricAttack());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _meleeAttackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _vampiricAttackRadius);
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

    private IEnumerator VampiricAttack()
    {
        _isVampiricAttacking = true;

        _vampiricTimer = _vampiricAttackDuration;

        while (_vampiricTimer != 0)
        {
            _vampiricAuraSprite.gameObject.SetActive(true);

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

            yield return new WaitForSeconds(_timerCoefficient);

            _vampiricTimer--;

            _vampiricDurationSlider.value = _vampiricTimer;
        }

        _isVampiricAttacking = false;
        _vampiricAuraSprite.gameObject.SetActive(false);
        _vampiricDurationSlider.value = _vampiricAttackDuration;
    }
}