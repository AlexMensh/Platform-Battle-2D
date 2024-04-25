using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMover _playerMover;

    private int _speedHash = Animator.StringToHash("Speed");
    private int _isJumpingHash = Animator.StringToHash("IsJumping");

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        PlayerMovementCheck();
        PlayerJumpingCheck();
    }

    private void PlayerMovementCheck()
    {
        if (_playerMover.IsOnGround)
        {
            _playerAnimator.SetFloat(_speedHash, Mathf.Abs(_playerMover.HorizontalInput));
        }
    }

    private void PlayerJumpingCheck()
    {
        _playerAnimator.SetBool(_isJumpingHash, _playerMover.IsOnGround == false);
    }
}