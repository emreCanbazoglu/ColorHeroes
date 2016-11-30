using UnityEngine;
using System.Collections;

public class CharMovementController : MonoBehaviour 
{
    public CharacterScript Character;

    public Rigidbody Rigidbody;

    public float AccForce;
    public float MaxSpeed;

    PlayerInputController _inputController;

    public void SetPlayerInputController(PlayerInputController inputController)
    {
        _inputController = inputController;
    }

    public void UpdateFrame()
    {
        CheckMovement();
    }

    void CheckMovement()
    {
        Vector2 inputDirection = Vector2.zero;

        inputDirection.x = Input.GetAxis(_inputController.InputInfo.MainHorAxis);
        inputDirection.y = Input.GetAxis(_inputController.InputInfo.MainVerAxis);

        Rigidbody.AddForce(inputDirection * AccForce, ForceMode.Acceleration);

        LimitSpeed();
    }

    void LimitSpeed()
    {
        if (Rigidbody.velocity.magnitude <= MaxSpeed)
            return;

        Vector2 moveDirection = Rigidbody.velocity.normalized;

        Vector3 newVelocity = moveDirection * MaxSpeed;

        Rigidbody.velocity = newVelocity;

    }
}
