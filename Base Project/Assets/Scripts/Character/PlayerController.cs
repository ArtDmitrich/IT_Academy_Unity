using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public UnityAction PlayerDied;

    [SerializeField] private float _animationBlendSpeed;
    [SerializeField] private Animator _animator;

    private Vector2 _inputDirection;
    private float _targetAnimationSpeed = 0.0f;
    private bool _isSprint = false;

    private bool _isJumping = false;

    private bool _isCanMoving = true;

    private CharacterMovement PlayerMovement { get { return _playerMovement = _playerMovement ?? GetComponent<CharacterMovement>(); } }
    private RigidbodiesController Ragdoll { get { return _ragdoll = _ragdoll ?? GetComponent<RigidbodiesController>(); } }

    private CharacterMovement _playerMovement;
    private RigidbodiesController _ragdoll;

    [Inject] private InputController _inputController;

    public void SetActiveForMovement(bool active)
    {
        _isCanMoving = active;
        PlayerMovement.enabled = active;
    }

    public void Death()
    {
        SetActiveForMovement(false);

        _animator.SetFloat("Speed", 0.0f);
        _animator.enabled = false;

        Ragdoll.SetIsKinematic(false);
        PlayerDied?.Invoke();
    }

    public void Respawn(Vector3 startPos)
    {
        transform.SetPositionAndRotation(startPos, Quaternion.identity);
        _animator.enabled = true;
        Ragdoll.SetIsKinematic(true);
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!_isCanMoving)
        {
            return;
        }

        _inputDirection = ctx.ReadValue<Vector2>();
        PlayerMovement.StartMovement(_inputDirection);
        SetAnimationSpeed();
    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!_isCanMoving)
        {
            return;
        }

        PlayerMovement.StopMovement();
        _inputDirection = Vector2.zero;
        SetAnimationSpeed();
    }

    private void ChangeIsSprinting(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!_isCanMoving)
        {
            return;
        }

        _isSprint = !_isSprint;
        PlayerMovement.IsSprint = _isSprint;
        SetAnimationSpeed();
    }

    private void SetAnimationSpeed()
    {
        if (_inputDirection != Vector2.zero)
        {
            _targetAnimationSpeed = _isSprint ? 1.0f : 0.5f;
        }
        else
        {
            _targetAnimationSpeed = 0f;
        }

        var _animationSpeed = Mathf.Lerp(_animator.GetFloat("Speed"), _targetAnimationSpeed, _animationBlendSpeed);
        _animator.SetFloat("Speed", _animationSpeed);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!_isJumping && _isCanMoving)
        {
            _isJumping = true;
            PlayerMovement.Jump();
            _animator.SetTrigger("Jump");
        }
    }

    private void Update()
    {
        if (!_isCanMoving)
        {
            return;
        }

        if (_isJumping && PlayerMovement.SpeedY < 0.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                _isJumping = false;
                _animator.SetTrigger("Land");
            }
        }
    }

    private void OnEnable()
    {
        _inputController.MainScene.Movement.performed += Movement_performed;
        _inputController.MainScene.Movement.canceled += Movement_canceled;
        _inputController.MainScene.Sprint.started += ChangeIsSprinting;
        _inputController.MainScene.Sprint.canceled += ChangeIsSprinting;
        _inputController.MainScene.Jump.performed += Jump_performed;
    }

    private void OnDisable()
    {
        _inputController.MainScene.Movement.performed -= Movement_performed;
        _inputController.MainScene.Movement.canceled -= Movement_canceled;
        _inputController.MainScene.Sprint.started -= ChangeIsSprinting;
        _inputController.MainScene.Sprint.canceled -= ChangeIsSprinting;
        _inputController.MainScene.Jump.performed -= Jump_performed;
    }
}
