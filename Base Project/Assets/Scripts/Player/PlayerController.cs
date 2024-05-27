using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterRotator))]
[RequireComponent(typeof(SoundController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Light _flashlight;

    private InputController Input { get { return _input = _input ?? new InputController(); } }
    private CharacterMovement PlayerMovement { get { return _playerMoovement = _playerMoovement ?? GetComponent<CharacterMovement>(); } }
    private CharacterRotator Rotator { get { return _rotator = _rotator ?? GetComponent<CharacterRotator>(); } }
    private SoundController SoundController { get { return _soundController = _soundController ?? GetComponent<SoundController>(); } }

    private InputController _input;
    private CharacterMovement _playerMoovement;
    private CharacterRotator _rotator;
    private SoundController _soundController;

    private void OnEnable()
    {
        Input.Enable();
        Input.MainScene.Rotation.performed += Rotation_performed;
        Input.MainScene.Flashlight.performed += Flashlight_performed;
    }

    private void OnDisable()
    {
        Input.Disable();
        Input.MainScene.Rotation.performed -= Rotation_performed;
        Input.MainScene.Flashlight.performed += Flashlight_performed;
    }

    private void Update()
    {
        PlayerMovement.Move(Input.MainScene.Movement.ReadValue<Vector2>());

        var playerVelocity = PlayerMovement.CharacterVelocityXZ;

        if (playerVelocity != Vector2.zero)
        {
            SoundController.LoopingPlay("Run", transform.position);
        }
        else
        {
            SoundController.Pause("Run");
        }
    }

    private void Flashlight_performed(InputAction.CallbackContext obj)
    {
        if (_flashlight != null)
        {
            _flashlight.enabled = !_flashlight.enabled;
            SoundController.OneShotPlay("Flashlight", transform.position);
        }
    }

    private void Rotation_performed(InputAction.CallbackContext obj)
    {
        Rotator.Rotate(obj.ReadValue<Vector2>());
    }
}
