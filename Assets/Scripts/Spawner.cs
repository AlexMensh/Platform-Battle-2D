using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxPowerups;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        for (int i = 0; i < _maxPowerups; i++)
        {
            int spawnerIndex = Random.Range(0, _spawnPoints.Length);

            if (_spawnPoints[spawnerIndex])
            {
                _spawnPoints[spawnerIndex].ActivatePowerup();
            }

            yield return wait;
        }
    }
}