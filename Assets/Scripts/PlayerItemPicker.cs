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
        if (collision.gameObject.GetComponent<Powerup>())
        {
            collision.gameObject.GetComponent<Powerup>().Heal(_player);
            collision.gameObject.SetActive(false);
        }
    }
}
