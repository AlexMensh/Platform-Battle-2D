using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _playerSpriteRenderer;
    private bool _isStartJump;
    private int _reversalNumber = 180;
    private float _horizontalInput;
    private string _horizontalInputName = "Horizontal";

    public Action<float> OnHorizontalChanged;
    public Action OnVerticalChanged;

    public bool IsOnGround { get; private set; } = true;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        Physics2D.gravity *= _gravityForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsOnGround = true;
        OnVerticalChanged?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsOnGround = false;
        OnVerticalChanged?.Invoke();
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
        _horizontalInput = Input.GetAxis(_horizontalInputName);

        if (_horizontalInput != 0)
        {
            OnHorizontalChanged?.Invoke(_horizontalInput);

            transform.position += new Vector3(_horizontalInput, 0f, 0f) * _speed * Time.deltaTime;

            if (_horizontalInput < 0)
                transform.rotation = Quaternion.Euler(0, _reversalNumber, 0);
            else if (_horizontalInput > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
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
}