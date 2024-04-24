using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMover _playerMoverScript;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMoverScript = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        PlayerMovementCheck();
        PlayerJumpingCheck();
    }

    private void PlayerMovementCheck()
    {
        if (_playerMoverScript.IsOnGround)
        {
            _playerAnimator.SetFloat("Speed", Mathf.Abs(_playerMoverScript.HorizontalInput));
        }
    }

    private void PlayerJumpingCheck()
    {
        if (_playerMoverScript.IsOnGround)
        {
            _playerAnimator.SetBool("IsJumping", false);
        }
        else
        {
            _playerAnimator.SetBool("IsJumping", true);
        }
    }
}