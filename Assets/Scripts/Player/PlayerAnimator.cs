using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(PlayerMeleeAttack))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMover _playerMover;
    private PlayerMeleeAttack _playerMeleeAttack;

    private int _speedHash = Animator.StringToHash("Speed");
    private int _isJumpingHash = Animator.StringToHash("IsJumping");
    private int _attackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _playerMeleeAttack = GetComponent<PlayerMeleeAttack>();
    }

    private void OnEnable()
    {
        _playerMover.HorizontalChanged += RunningPlay;
        _playerMover.VerticalChanged += JumpingPlay;
        _playerMeleeAttack.Attacked += AttackPlay;
    }

    private void OnDisable()
    {
        _playerMover.HorizontalChanged -= RunningPlay;
        _playerMover.VerticalChanged -= JumpingPlay;
        _playerMeleeAttack.Attacked -= AttackPlay;
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