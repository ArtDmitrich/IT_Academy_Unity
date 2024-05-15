using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }

    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private bool groundedCharacter;
    private Vector3 _characterMotion;

    private CharacterController _controller;
    private Controls _input;

    private void Start()
    {
        _input = new Controls();
        _input.Enable();
    }

    private void Update()
    {
        var inputMovement = _input.MainScene.Movement.ReadValue<Vector2>();
        _characterMotion.x = inputMovement.x;
        _characterMotion.z = inputMovement.y;


        Controller.Move(transform.TransformDirection(new Vector3 (_characterMotion.x, _gravity, _characterMotion.z)) * _movementSpeed * Time.deltaTime);


        if (groundedCharacter && _input.MainScene.Jump.triggered)
        {
            _characterMotion.y += _jumpHeight;
            Debug.Log($"Jump, {_characterMotion.y}");
        }
        else
        {
            _characterMotion.y += _gravity;
        }

        _controller.Move(Vector3.up * _characterMotion.y);
        groundedCharacter = Controller.isGrounded;

        var rotatinon = _input.MainScene.Rotation.ReadValue<float>();
        transform.Rotate(Vector3.up * rotatinon * _rotationSpeed);
    }
}
