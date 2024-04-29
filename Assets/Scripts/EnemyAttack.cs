using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;

    public Action OnAttacked;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }

    public void Attack(Player player)
    {
        player.TakeDamage(_attackPower);
        OnAttacked?.Invoke();
    }
}
