using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityAction Turned;
    public UnityAction<float> SprintStarted;
    public UnityAction<float> SprintCanceled;

    public static GameController Instance { get; private set; }

    [SerializeField] private float _sprintSpeedMultiplier;

    private InputController Input { get { return _input = _input ?? new InputController(); } }
    private InputController _input;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        Input.Enable();
        Input.Ninja.Turn.performed += Turn_performed;
        Input.Ninja.Sprint.started += Sprint_started;
        Input.Ninja.Sprint.canceled += Sprint_canceled;

    }


    private void OnDisable()
    {
        Input.Disable();
        Input.Ninja.Turn.performed -= Turn_performed;
        Input.Ninja.Sprint.started -= Sprint_started;
        Input.Ninja.Sprint.canceled -= Sprint_canceled;
    }

    private void Turn_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Turned?.Invoke();
    }

    private void Sprint_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SprintCanceled?.Invoke(_sprintSpeedMultiplier);
    }

    private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SprintStarted?.Invoke(1f);
    }
}
