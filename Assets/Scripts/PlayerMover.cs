using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _playerSpriteRenderer;
    private string _horizontalInputName = "Horizontal";

    public static Action OnVerticalPositionChange;
    public static Action OnHorizontalPositionChange;

    public float HorizontalInput { get; private set; }
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
        OnVerticalPositionChange?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsOnGround = false;
        OnVerticalPositionChange?.Invoke();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        HorizontalInput = Input.GetAxis(_horizontalInputName);

        if (HorizontalInput != 0)
        {
            OnHorizontalPositionChange?.Invoke();

            transform.position += new Vector3(HorizontalInput, 0f, 0f) * _speed * Time.deltaTime;

            _playerSpriteRenderer.flipX = HorizontalInput < 0 ? true : false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}