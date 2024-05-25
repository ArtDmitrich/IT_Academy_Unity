using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private float _gravity;
    [SerializeField] private float _minGravity;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }
    private InputController Input { get { return _input = _input ?? new InputController(); } }

    private CharacterController _controller;
    private InputController _input;

    private bool _isGrounded;
    private Vector3 _playerVelocity;

    private void OnEnable()
    {
        Input.Enable();
        Input.MainScene.Rotation.performed += Rotation_performed;
    }

    private void Update()
    {
        ApplyGravity();
        Move();
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _playerVelocity.y < 0f)
        {
            _playerVelocity.y = _minGravity;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
    }

    private void Move()
    {
        var inputMotion = Input.MainScene.Movement.ReadValue<Vector2>();
        var resultMotion = transform.TransformDirection(new Vector3(inputMotion.x, 0f, inputMotion.y));

        _isGrounded = Controller.isGrounded;
        Controller.Move((resultMotion + _playerVelocity) * _movementSpeed * Time.deltaTime);
    }

    private void Rotation_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var inputRotation = obj.ReadValue<Vector2>();
        transform.Rotate(0f, inputRotation.x * _rotationSpeed, 0f);

        if (_cameraRotator != null)
        {
            _cameraRotator.VerticalRotate(-inputRotation.y);
        }
    }

    private void OnDisable()
    {
        Input.Disable();
        Input.MainScene.Rotation.performed -= Rotation_performed;
    }
}
