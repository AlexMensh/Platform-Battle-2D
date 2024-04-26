using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMover _playerMover;

    private int _speedHash = Animator.StringToHash("Speed");
    private int _isJumpingHash = Animator.StringToHash("IsJumping");

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.OnHorizontalChanged += RunningPlay;
        _playerMover.OnVerticalChanged += JumpingPlay;
    }

    private void OnDisable()
    {
        _playerMover.OnHorizontalChanged -= RunningPlay;
        _playerMover.OnVerticalChanged -= JumpingPlay;
    }

    private void RunningPlay(float horizontalInput)
    {
        _playerAnimator.SetFloat(_speedHash, Mathf.Abs(Mathf.Round(horizontalInput)));
    }

    private void JumpingPlay()
    {
        _playerAnimator.SetBool(_isJumpingHash, _playerMover.IsOnGround == false);
    }
}