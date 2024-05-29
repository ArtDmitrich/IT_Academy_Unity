using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public UnityAction<Vector2> PlayerMoveningStarted;
    public UnityAction PlayerMoveningStoped;
    public UnityAction PlayerJumped;

    private InputController Input { get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        Input.Enable();

        Input.MainScenePlayerInput.Movement.started += Movement_started;
        Input.MainScenePlayerInput.Movement.canceled += Movement_canceled;
        Input.MainScenePlayerInput.Jump.performed += Jump_performed;
    }

    private void OnDisable()
    {
        Input.Disable();

        Input.MainScenePlayerInput.Movement.started -= Movement_started;
        Input.MainScenePlayerInput.Movement.canceled -= Movement_canceled;
        Input.MainScenePlayerInput.Jump.performed -= Jump_performed;
    }

    private void Movement_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerMoveningStarted?.Invoke(obj.ReadValue<Vector2>());
    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerMoveningStoped?.Invoke();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayerJumped?.Invoke();
    }
}
