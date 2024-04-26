using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private SpriteRenderer _playerSpriteRenderer;
    private int _randomWaypointIndex;
    private int _secondPointIndex = 1;
    private float _endDetectionOffset = 0.2f;

    private void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _randomWaypointIndex = Random.Range(0, _waypoints.Length);
    }

    private void Update()
    {
        Patrol();
    }

    private void ChooseRandomPoint()
    {
        _randomWaypointIndex = Random.Range(0, _waypoints.Length);
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_randomWaypointIndex].transform.position, _speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, _waypoints[_randomWaypointIndex].transform.position) < _endDetectionOffset)
        {
            _playerSpriteRenderer.flipX = _randomWaypointIndex == _secondPointIndex;
            ChooseRandomPoint();
        }
    }
}