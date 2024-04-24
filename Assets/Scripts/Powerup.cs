using UnityEngine;

public class Powerup : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMover>())
        {
            gameObject.SetActive(false);
        }
    }
}