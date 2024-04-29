
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;

    public void Attack(Player player)
    {
        player.TakeDamage(_attackPower);
    }
}
