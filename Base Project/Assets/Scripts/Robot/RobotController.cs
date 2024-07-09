using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RobotController : MonoBehaviour
{
    public bool IsCanShoot;
    public BulletType CurrentBulletType;

    [SerializeField] private PhysMover _mover;
    [SerializeField] private Rotator _rotatingGunBase;

    [SerializeField] private Shooter _shooter;

    [Inject] private PoolsManager _poolsManager;

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
            //var item = _poolsManager.GetPooledItem(CurrentBulletType.ToString());
            var item = PoolsManager.Instance.GetPooledItem(CurrentBulletType.ToString());

            if (item != null && item.TryGetComponent<Bullet>(out var bullet))
            {
                _shooter.Shoot(bullet);
            }
        }
    }
}
