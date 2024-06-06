using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private Animator _animator;

    private IMovable PlayerMovement { get { return _playerMovement = _playerMovement ?? GetComponent<IMovable>(); } }
    private IMovable _playerMovement;

    private InputController Input {  get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    //private Vector2 _currentDirection = Vector2.right;

    private void OnEnable()
    {
        Input.Enable();
        Input.PlayerInput.Movement.started += Movement_started;
        Input.PlayerInput.Movement.canceled += Movement_canceled;
    }

    private void OnDisable()
    {
        Input.Disable();
        Input.PlayerInput.Movement.started -= Movement_started;
        Input.PlayerInput.Movement.canceled -= Movement_canceled;
    }

    private void FixedUpdate()
    {
        var direction = Input.PlayerInput.Movement.ReadValue<Vector2>();

        PlayerMovement.Move(direction);
    }

    private void Movement_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //_animator.SetTrigger("StartMovement");
    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //_animator.SetTrigger("StopMovement");
    }
}
