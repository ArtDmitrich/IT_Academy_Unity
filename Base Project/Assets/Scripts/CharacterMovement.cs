using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public float SpeedY { get { return _speedY; } }
    public float JumpSpeed { get { return _jumpSpeed; } }

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _controller;
    public CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }

    private float _rotationAngel = 0.0f;
    private float _speedY = 0.0f;

    public void Move(Vector2 input, float rotationY, bool isSprint)
    {
        ApplyGravity();
        MoveCharacter(input, rotationY, isSprint);
    }

    public void Jump()
    {
        _speedY += _jumpSpeed;
    }

    private void ApplyGravity()
    {
        if (!Controller.isGrounded)
        {
            _speedY += _gravity * Time.deltaTime;
        }
        else if (_speedY < 0.0f)
        {
            _speedY = 0.0f;
        }
    }

    private void MoveCharacter(Vector2 input, float rotationY, bool isSprint)
    {
        Vector3 movement = new Vector3(input.x, 0.0f, input.y);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, rotationY, 0.0f) * movement.normalized;
        Vector3 verticalMovemnt = Vector3.up * _speedY;

        float currentSpeed = isSprint ? _sprintSpeed : _walkSpeed;
        Controller.Move((verticalMovemnt + rotatedMovement * currentSpeed) * Time.deltaTime);

        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            _rotationAngel = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }

        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngel, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed);
    }

}
