using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _pursuitSpeed;
    [SerializeField] private float _detectRadius;

    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private Transform _nextPoint;
    private bool _isDetected = false;
    private float _endPointOffset = 0.2f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _nextPoint = _endPoint;
    }

    private void Update()
    {
        if (_isDetected == false)
        {
            Patrol();
            DetectPlayer();
        }
        else
        {
            Pursue();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, _nextPoint.transform.position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _nextPoint.transform.position) < _endPointOffset)
        {
            if (_nextPoint == _endPoint)
            {
                ChooseStartPointNext();
            }
            else
            {
                ChooseEndPointNext();
            }
        }
    }

    private void Pursue()
    {
        if (_playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _pursuitSpeed * Time.deltaTime);

            if (transform.position.x < _startPoint.transform.position.x)
            {
                _isDetected = false;
                ChooseStartPointNext();
            }
            else if (transform.position.x > _endPoint.transform.position.x)
            {
                _isDetected = false;
                ChooseEndPointNext();
            }

            _spriteRenderer.flipX = transform.position.x > _playerTransform.position.x;
        }
        else
        {
            _isDetected = false;
        }
    }

    private void DetectPlayer()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRadius);

        if (target.TryGetComponent(out Player player))
        {
            _playerTransform = player.transform;
            _isDetected = true;
        }
    }

    private void ChooseStartPointNext()
    {
        _spriteRenderer.flipX = true;
        _nextPoint = _startPoint;
    }

    private void ChooseEndPointNext()
    {
        _spriteRenderer.flipX = false;
        _nextPoint = _endPoint;
    }
}