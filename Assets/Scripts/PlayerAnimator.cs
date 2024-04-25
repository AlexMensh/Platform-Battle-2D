using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover))]
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

    private void OnEnable()
    {
        PlayerMover.OnHorizontalPositionChange += RunningPlay;
        PlayerMover.OnVerticalPositionChange += JumpingPlay;
    }

    private void OnDisable()
    {
        PlayerMover.OnHorizontalPositionChange -= RunningPlay;
        PlayerMover.OnVerticalPositionChange -= JumpingPlay;
    }

    private void RunningPlay()
    {
        if (_playerMover.IsOnGround)
        {
            _playerAnimator.SetFloat(_speedHash, Mathf.Abs(Mathf.Round(_playerMover.HorizontalInput)));
        }
    }

    private void JumpingPlay()
    {
        bool isJumping = !_playerAnimator.GetBool(_isJumpingHash);
        _playerAnimator.SetBool(_isJumpingHash, isJumping);
    }
}