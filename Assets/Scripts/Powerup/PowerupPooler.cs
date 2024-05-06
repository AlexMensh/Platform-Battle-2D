using UnityEngine;

public class PowerupPooler : MonoBehaviour
{
    [SerializeField] private PowerupPoint[] _poolPoints;

    private void Start()
    {
        Pool();
    }

    private void Pool()
    {
        for (int i = 0; i < _poolPoints.Length; i++)
        {
            _poolPoints[i].ActivatePowerup();
        }
    }
}