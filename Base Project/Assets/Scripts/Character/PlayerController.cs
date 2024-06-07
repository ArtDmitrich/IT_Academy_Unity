using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private IMovable PlayerMovement { get { return _playerMovement = _playerMovement ?? GetComponent<IMovable>(); } }
    private IMovable _playerMovement;

    private InputController Input {  get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    private IUsable _usableComponent;

    private void OnEnable()
    {
        Input.Enable();

        Input.PlayerInput.Movement.performed += Movement_performed;
        Input.PlayerInput.Movement.canceled += Movement_canceled;
        Input.PlayerInput.Use.performed += Use_performed;
    }

    private void OnDisable()
    {
        Input.Disable();

        Input.PlayerInput.Movement.performed -= Movement_performed;
        Input.PlayerInput.Movement.canceled -= Movement_canceled;
        Input.PlayerInput.Use.performed -= Use_performed;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var direction = obj.ReadValue<Vector2>();

        PlayerMovement.StartMovement(direction);
        _animator.SetBool("Walk", true);
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);

    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerMovement.StopMovement();
        _animator.SetBool("Walk", false);
    }
    private void Use_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_usableComponent != null)
        {
            _usableComponent.Use();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _usableComponent = collision.GetComponent<IUsable>();
    }

    private void OnTriggerExit2D()
    {
        _usableComponent = null;
    }
}
