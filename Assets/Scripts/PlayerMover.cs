using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _playerSpriteRenderer;

    public float HorizontalInput { get; private set; }
    public bool IsOnGround { get; private set; } = true;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

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
        HorizontalInput = Input.GetAxis("Horizontal");

        _playerSpriteRenderer.flipX = HorizontalInput < 0 ? true : false;

        return new Vector3 (HorizontalInput, 0f, 0f);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround) 
        { 
            _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            IsOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround = true;
    }
}
