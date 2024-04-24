using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Powerup _powerpupPrefab;

    public void ActivatePowerup()
    {
        _powerpupPrefab.gameObject.SetActive(true);
    }
}