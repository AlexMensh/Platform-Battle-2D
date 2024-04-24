using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    [SerializeField] private bool _isOnGround = true;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= _gravityForce;
    }

    private void Update()
    {
        Move(GetMoveDirection());
        Jump();
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }

    private Vector3 GetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        return new Vector3 (horizontalInput, 0f, 0f);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround) 
        { 
            _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isOnGround = true;
    }
}
