using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Powerup>())
        {
            collision.gameObject.SetActive(false);
        }
    }
}
