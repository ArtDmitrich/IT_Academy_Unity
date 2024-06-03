using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public UnityAction GameOver;
    public UnityAction FinishedLevel;

    public UnityAction<Vector2> PlayerMoveningStarted;
    public UnityAction PlayerMoveningStoped;
    public UnityAction PlayerJumped;

    [SerializeField] private Transform _startPlayerPos;
    [SerializeField] private int _startPlayerLives;

    [SerializeField] private List<Transform> _enemySpots;
    [SerializeField] private List<Transform> _coinSpots;

    [SerializeField] private CanvasController _canvas;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private ResourceController _resourceController;

    private InputController Input { get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    public void ChangePlayerLivesCount(int value)
    {
        if (value < 0)
        {
            RestartLevel();
        }

        if (_resourceController != null)
        {
            _resourceController.PlayerLives = value;
        }
    }

    public void ChangeCoinsCount(int value)
    {
        if (_resourceController != null)
        {
            _resourceController.CoinsCount = value;
        }
    }

    public void FinishLevel()
    {
        Input.PlayerInput.Disable();
        FinishedLevel?.Invoke();
        _canvas.FinishGame(true);
    }

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

        Input.PlayerInput.Movement.started += Movement_started;
        Input.PlayerInput.Movement.canceled += Movement_canceled;
        Input.PlayerInput.Jump.performed += Jump_performed;

        if (_resourceController != null)
        {
            _resourceController.PlayerDeaded += StopLevel;
        }
        else
        {
            Debug.LogWarning("ResourceController is null");
        }

        if (_canvas != null && _canvas.Start != null)
        {
            _canvas.Start.onClick.AddListener(StartLevel);
        }
        else
        {
            Debug.LogWarning("CanvasController or button is null");
        }
    }

    private void OnDisable()
    {
        Input.Disable();

        Input.PlayerInput.Movement.started -= Movement_started;
        Input.PlayerInput.Movement.canceled -= Movement_canceled;
        Input.PlayerInput.Jump.performed -= Jump_performed;

        if (_resourceController != null)
        {
            _resourceController.PlayerDeaded -= StopLevel;
        }        

        if (_canvas != null && _canvas.Start != null)
        {
            _canvas.Start.onClick.RemoveListener(StartLevel);
        }
    }

    private void StartLevel()
    {
        _canvas.StartGame();

        SetPlayerToStartPos();
        SetItemsToSpots("Enemy", _enemySpots);
        SetItemsToSpots("Coin", _coinSpots);

        ChangePlayerLivesCount(_startPlayerLives);
        ChangeCoinsCount(0);

        Input.PlayerInput.Enable();
    }

    private void RestartLevel()
    {
        _objectPool.DeactivatePooledItems("Player");
        _objectPool.DeactivatePooledItems("Enemy");

        SetPlayerToStartPos();
        SetItemsToSpots("Enemy", _enemySpots);
    }

    private void StopLevel()
    {
        Input.PlayerInput.Disable();
        GameOver?.Invoke();
        _canvas.FinishGame(false);
    }

    private void SetPlayerToStartPos()
    {
        var player = _objectPool.GetPooledItem("Player");
        player.transform.position = _startPlayerPos.position;

        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
    }

    private void SetItemsToSpots(string itemName, List<Transform> spots)
    {
        foreach (var spot in spots)
        {
            var item = _objectPool.GetPooledItem(itemName);
            item.transform.position = spot.position;
            item.transform.SetParent(spot);
        }
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
