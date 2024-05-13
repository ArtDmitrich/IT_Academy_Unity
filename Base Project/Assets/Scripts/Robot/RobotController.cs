using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [SerializeField] private PhysMover _mover;
    [SerializeField] private Rotator _rotatingGunBase;

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

    private void Update()
    {
        var gunRotationInput = _input.MainScene.GunRotation.ReadValue<float>();

        if (gunRotationInput != 0f)
        {
            _rotatingGunBase.Rotate(gunRotationInput);
        }        
    }

    private void FixedUpdate()
    {
        var movementInput = _input.MainScene.Movement.ReadValue<float>();

        if (movementInput != 0f)
        {
            _mover.Move(movementInput);
        }

        var rotationInput = _input.MainScene.Rotation.ReadValue<float>();

        if (rotationInput != 0f)
        {
            _mover.Rotate(rotationInput);
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
