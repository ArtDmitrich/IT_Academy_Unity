using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _animationBlendSpeed;


    private CharacterController _controller;
    private Animator _animator;
    private Camera _characterCamera;

    private float _rotationAngel = 0.0f;
    private float _targetAnimationSpeed = 0.0f;
    private bool _isSprint = false;

    private float _speedY = 0.0f;
    private float _gravity = -9.81f;
    private bool _isJumping = false;

    public CharacterController Controller { get { return _controller = _controller ?? GetComponent<CharacterController>(); } }
    public Animator CharacterAnimator { get { return _animator = _animator ?? GetComponent<Animator>(); } }

    public Camera CharacterCamera { get { return _characterCamera = _characterCamera ?? FindObjectOfType<Camera>(); } }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _isJumping = true;
            _speedY += _jumpSpeed;
            CharacterAnimator.SetTrigger("Jump");
        }

        if (!Controller.isGrounded)
        {
            _speedY += _gravity * Time.deltaTime;
        }
        else if (_speedY < 0.0f)
        {
            _speedY = 0.0f;
        }

        CharacterAnimator.SetFloat("SpeedY", _speedY / _jumpSpeed);

        if (_isJumping && _speedY < 0.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                _isJumping = false;
                CharacterAnimator.SetTrigger("Load");
            }
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        _isSprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(horizontal, _speedY, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;

        float currentSpeed = _isSprint ? _sprintSpeed : _walkSpeed;
        Controller.Move(rotatedMovement * currentSpeed * Time.deltaTime);

        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            _rotationAngel = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            _targetAnimationSpeed = _isSprint ? 1.0f : 0.5f;
        }
        else
        {
            _targetAnimationSpeed = 0f;
        }

        float animationSpeed = Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), _targetAnimationSpeed, _animationBlendSpeed);
        CharacterAnimator.SetFloat("Speed", animationSpeed);

        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngel, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed);
    }
}
