using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [SerializeField] private PhysMover _mover;

    [SerializeField] private Shooter _shooter;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private BulletType _currentBulletType;
    private bool _isShooting;

    private GameInput _input;

    public void SetBulletType(BulletType type)
    {
        _currentBulletType = type;
    }

    public void SetPosibilityOfShooting(bool isCanShooting)
    {
        _isShooting = isCanShooting;
    }

    private void Start()
    {
        _isShooting = false;

        _input = new GameInput();
        _input.Enable();

        _input.MainScene.Shoot.performed += OnShootPerformed;
    }

    private void FixedUpdate()
    {
        var horInput = _input.MainScene.Movement.ReadValue<float>();
        var verInput = _input.MainScene.Rotation.ReadValue<float>();

        if (horInput != 0f)
        {
            _mover.Move(horInput);
        }

        if (verInput != 0f)
        {
            _mover.Rotate(verInput);
        }
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.MainScene.Shoot.performed -= OnShootPerformed;
        }
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (_isShooting)
        {
            _shooter.Shoot(_bulletSpawner.GetBullet(_currentBulletType, _gunPos));
        }
    }
}
