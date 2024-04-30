using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int _healPoints;

    public void Heal(Player player)
    {
        player.RestoreHealth(_healPoints);
    }
}