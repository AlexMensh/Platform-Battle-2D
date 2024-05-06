using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(PlayerAttack))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMover _playerMover;
    private PlayerAttack _playerAttack;

    private int _speedHash = Animator.StringToHash("Speed");
    private int _isJumpingHash = Animator.StringToHash("IsJumping");
    private int _attackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnEnable()
    {
        _playerMover.HorizontalChanged += RunningPlay;
        _playerMover.VerticalChanged += JumpingPlay;
        _playerAttack.BeenAttacked += AttackPlay;
    }

    private void OnDisable()
    {
        _playerMover.HorizontalChanged -= RunningPlay;
        _playerMover.VerticalChanged -= JumpingPlay;
        _playerAttack.BeenAttacked -= AttackPlay;
    }

    private void RunningPlay(float horizontalInput)
    {
        _playerAnimator.SetFloat(_speedHash, Mathf.Abs(Mathf.Round(horizontalInput)));
    }

    private void JumpingPlay()
    {
        _playerAnimator.SetBool(_isJumpingHash, _playerMover.IsOnGround == false);
    }

    private void AttackPlay()
    {
        _playerAnimator.SetTrigger(_attackHash);
    }
}