using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerItemPicker : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Powerup powerup))
        {
            powerup.Heal(_player);
            powerup.gameObject.SetActive(false);
        }
    }
}