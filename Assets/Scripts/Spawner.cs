using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxRoundPowerups;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        for (int i = 0; i < _maxRoundPowerups; i++)
        {
            int spawnerIndex = Random.Range(0, _spawnPoints.Length);

            _spawnPoints[spawnerIndex].ActivatePowerup();

            yield return wait;
        }
    }
}