using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public Vector2 CharacterVelocityXZ { get { return new Vector2(Controller.velocity.x, Controller.velocity.z); } }

    [SerializeField] private float _gravity;
    [SerializeField] private float _minGravity;
    [SerializeField] private float _movementSpeed;

    private CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }

    private CharacterController _controller;

    private float _playerVelocityY;
    private bool _isGrounded;

    public void Move(Vector2 input)
    {
        ApplyGravity();
        MoveCharacter(input);
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _playerVelocityY < 0f)
        {
            _playerVelocityY = _minGravity;
        }

        _playerVelocityY += _gravity * Time.deltaTime;
    }

    private void MoveCharacter(Vector2 inputMotion)
    {
        var resultMotion = transform.TransformDirection(new Vector3(inputMotion.x, 0f, inputMotion.y));
        _isGrounded = Controller.isGrounded;
        Controller.Move((resultMotion + Vector3.up *_playerVelocityY) * _movementSpeed * Time.deltaTime);
    }
}
