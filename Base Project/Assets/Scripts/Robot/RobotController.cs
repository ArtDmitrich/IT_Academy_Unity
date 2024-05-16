using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    public bool IsCanShoot { get; set; }
    public BulletType CurrentBulletType { get; set; }

    [SerializeField] private PhysMover _mover;
    [SerializeField] private Rotator _rotatingGunBase;

    [SerializeField] private Shooter _shooter;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private GameInput _input;

    private void Start()
    {
        IsCanShoot = false;

        _input = new GameInput();
        _input.Enable();

        _input.MainScene.Shoot.performed += OnShootPerformed;
        _input.MainScene.GunRotation.performed += GunRotation_performed;
    }

    private void GunRotation_performed(InputAction.CallbackContext obj)
    {
        if (_rotatingGunBase != null)
        {
            _rotatingGunBase.Rotate(obj.ReadValue<float>());
        }
    }

    private void FixedUpdate()
    {
        var movementInput = _input.MainScene.Movement.ReadValue<float>();

        if (movementInput != 0f && _mover != null)
        {
            _mover.Move(movementInput);
        }

        var rotationInput = _input.MainScene.Rotation.ReadValue<float>();

        if (rotationInput != 0f && _mover != null)
        {
            _mover.Rotate(rotationInput);
        }
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.MainScene.Shoot.performed -= OnShootPerformed;
            _input.MainScene.GunRotation.performed -= GunRotation_performed;
        }
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (IsCanShoot && _shooter != null)
        {
            _shooter.Shoot(_bulletSpawner.GetBullet(CurrentBulletType, _gunPos));
        }
    }
}
