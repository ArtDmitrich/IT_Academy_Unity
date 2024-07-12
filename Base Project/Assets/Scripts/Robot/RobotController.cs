using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    public bool IsCanShoot;
    public BulletType CurrentBulletType;

    [SerializeField] private PhysMover _mover;
    [SerializeField] private Rotator _rotatingGunBase;

    [SerializeField] private Shooter _shooter;

    [SerializeField] private string _shootSoundName;

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
            var bullet = BulletManager.Instance.GetBullet(CurrentBulletType.ToString());

            if (bullet != null)
            {
                _shooter.Shoot(bullet);

                var _shootSound = AudioManager.Instance.GetSound(_shootSoundName);

                if (_shootSound != null)
                {
                    _shootSound.transform.position = transform.position;
                    _shootSound.Play();
                }
            }
        }
    }
}
