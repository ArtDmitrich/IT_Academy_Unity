using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool IsSprint;
    public float SpeedY { get { return _speedY; } }
    public float JumpSpeed { get { return _jumpSpeed; } }

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _controller;
    public CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }

    private float _speedY = 0.0f;

    private Vector2 _direction;
    private bool _isMoving;

    private void OnEnable()
    {
        Controller.enabled = true;
    }

    private void OnDisable()
    {
        Controller.enabled = false;
        _direction = Vector2.zero;
    }

    private void Update()
    {
        ApplyGravity();
        MoveCharacter();
        RotateCharacter();
    }

    public void StartMovement(Vector2 direction)
    {
        _isMoving = true;
        _direction = direction;
    }

    public void StopMovement()
    {
        _isMoving = false;
        _direction = Vector2.zero;
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

    private void MoveCharacter()
    {
        Vector3 resultMovemnt = Vector3.up * _speedY;

        if (_isMoving)
        {
            Vector3 movement = transform.forward * _direction.y;
            float currentSpeed = IsSprint ? _sprintSpeed : _walkSpeed;
            movement *= currentSpeed;

            resultMovemnt += movement;
        }

        Controller.Move(resultMovemnt * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        if (_direction == Vector2.zero)
        {
            return;
        }

        transform.Rotate(0f, _direction.x * _rotationSpeed * Time.deltaTime, 0f);
    }
}
