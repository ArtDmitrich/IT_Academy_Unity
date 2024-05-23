using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _animationBlendSpeed;
        
    private CharacterMovement _playerMovement;
    private Animator _animator;
    private AnimationClips _animationNames;
    private Camera _characterCamera;
    private InputController _inputController;

    private float _targetAnimationSpeed = 0.0f;
    private bool _isSprint = false;

    private bool _isJumping = false;

    private bool _isAlive = false;
    private bool _isAttacking = false;

    public CharacterMovement PlayerMovement { get { return _playerMovement = _playerMovement ?? GetComponent<CharacterMovement>(); } }
    public Animator CharacterAnimator { get { return _animator = _animator ?? GetComponent<Animator>(); } }
    public AnimationClips AnimationNames { get { return _animationNames = _animationNames ?? FindObjectOfType<AnimationClips>(); } }
    public Camera CharacterCamera { get { return _characterCamera = _characterCamera ?? FindObjectOfType<Camera>(); } }
    public InputController InputController { get { return _inputController = _inputController ?? new InputController(); } }

    private void OnEnable()
    {
        StartCoroutine(Spawn());

        InputController.Enable();

        InputController.MainScene.Attack.performed += Attack_performed;
        InputController.MainScene.Death.performed += Death_performed;
        InputController.MainScene.Sprint.started += ChangeIsSprinting;
        InputController.MainScene.Sprint.canceled += ChangeIsSprinting;
        InputController.MainScene.Jump.performed += Jump_performed;

    }

    private void OnDisable()
    {
        _isAlive = false;

        InputController.Disable();

        InputController.MainScene.Attack.performed -= Attack_performed;
        InputController.MainScene.Death.performed -= Death_performed;
        InputController.MainScene.Sprint.started -= ChangeIsSprinting;
        InputController.MainScene.Sprint.canceled -= ChangeIsSprinting;
        InputController.MainScene.Jump.performed -= Jump_performed;
    }

    private void Update()
    {
        if (!_isAlive || _isAttacking)
        {
            return;
        }

        var inputMovement = InputController.MainScene.Movement.ReadValue<Vector2>();
        var rotationY = CharacterCamera.transform.rotation.eulerAngles.y;

        PlayerMovement.Move(inputMovement, rotationY, _isSprint);

        var speedY = PlayerMovement.SpeedY;
        var jumpSpeed = PlayerMovement.JumpSpeed;
        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);

        if (_isJumping && speedY < 0.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                _isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        if (inputMovement.sqrMagnitude > 0.0f)
        {
            _targetAnimationSpeed = _isSprint ? 1.0f : 0.5f;
        }
        else
        {
            _targetAnimationSpeed = 0f;
        }

        float animationSpeed = Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), _targetAnimationSpeed, _animationBlendSpeed);
        CharacterAnimator.SetFloat("Speed", animationSpeed);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!_isJumping)
        {
            _isJumping = true;
            PlayerMovement.Jump();
            CharacterAnimator.SetTrigger("Jump");
        }
    }

    private void ChangeIsSprinting(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isSprint = !_isSprint;
    }

    private void Death_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isAlive)
        {
            _isAlive = false;
            CharacterAnimator.SetTrigger("Death");
        }
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!_isAttacking && _isAlive)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Spawn()
    {
        CharacterAnimator.SetTrigger("Spawn");

        yield return new WaitForSeconds(AnimationNames.GetAnimationLength("Spawn"));

        _isAlive = true;
    }

    private IEnumerator Attack()
    {
        CharacterAnimator.SetTrigger("Attack");

        var index = Random.Range(1, AnimationNames.AttacksCount + 1);
        CharacterAnimator.SetInteger("AttackIndex",index);
        _isAttacking = true;

        yield return new WaitForSeconds(AnimationNames.GetAnimationLength("Attack" + index));

        _isAttacking = false;
    }
}
