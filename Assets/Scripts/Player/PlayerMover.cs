using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _playerRigidbody;
    private PlayerInput _playerInput;
    private bool _isStartJump;
    private bool _isFacingRight = true;
    private float _horizontalInput;

    public event Action<float> HorizontalChanged;
    public event Action VerticalChanged;

    public bool IsOnGround { get; private set; } = true;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerInput = GetComponent<PlayerInput>();

        Physics2D.gravity *= _gravityForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround = true;
        VerticalChanged?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            IsOnGround = false;
            VerticalChanged?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        ApplyJumpForce();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _horizontalInput = _playerInput.GetHorizontalInput();

        if (_horizontalInput != 0)
        {
            HorizontalChanged?.Invoke(_horizontalInput);

            transform.position += new Vector3(_horizontalInput, 0f, 0f) * _speed * Time.deltaTime;

            if (_horizontalInput < 0 && _isFacingRight)
            {
                Flip();
            }
            else if (_horizontalInput > 0 && !_isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Jump()
    {
        if (_playerInput.IsJumpKeyPressed() && IsOnGround)
        {
            _isStartJump = true;
        }
    }

    private void ApplyJumpForce()
    {
        if (_isStartJump)
        {
            _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isStartJump = false;
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _spriteRenderer.flipX = !_isFacingRight;
    }
}