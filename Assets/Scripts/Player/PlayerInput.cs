using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalInputName = "Horizontal";

    public bool IsMeleeAttackKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    public bool IsVampiricAttackKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.CapsLock);
    }

    public float GetHorizontalInput()
    {
        return Input.GetAxis(_horizontalInputName);
    }

    public bool IsJumpKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

}
