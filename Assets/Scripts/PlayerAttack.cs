using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower;

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_attackPower);
    }
}
