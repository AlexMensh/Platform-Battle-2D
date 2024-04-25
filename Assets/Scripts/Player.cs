using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Powerup>())
        {
            collision.gameObject.SetActive(false);
        }
    }
}
