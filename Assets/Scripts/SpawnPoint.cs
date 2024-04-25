using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Powerup _powerpup;
        
    public void ActivatePowerup()
    {
        if (_powerpup.isActiveAndEnabled == false)
        {
            _powerpup.gameObject.SetActive(true);
        }
    }
}