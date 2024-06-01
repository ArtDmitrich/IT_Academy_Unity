using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
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

        _player.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Input.Enable();

        Input.MainScene.Movement.started += Movement_started;
        Input.MainScene.Movement.canceled += Movement_canceled;
        Input.MainScene.Jump.performed += Jump_performed;
    }

    private void OnDisable()
    {
        Input.Disable();

        Input.MainScene.Movement.started -= Movement_started;
        Input.MainScene.Movement.canceled -= Movement_canceled;
        Input.MainScene.Jump.performed -= Jump_performed;
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
