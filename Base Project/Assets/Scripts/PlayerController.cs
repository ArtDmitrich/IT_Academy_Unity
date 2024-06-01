using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _maxVelocity;
    [SerializeField] private float _movementForce;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D Rb { get { return _rb = _rb ?? GetComponent<Rigidbody2D>(); } }
    private Rigidbody2D _rb;

    private bool _isMoving;
    private Vector2 _movingDirection;

    private void OnEnable()
    {
        GameController.Instance.PlayerMoveningStarted += StartMovement;
        GameController.Instance.PlayerMoveningStoped += StopMovement;
        GameController.Instance.PlayerJumped += Jump;
    }

    private void OnDisable()
    {
        GameController.Instance.PlayerMoveningStarted -= StartMovement;
        GameController.Instance.PlayerMoveningStoped -= StopMovement;
        GameController.Instance.PlayerJumped -= Jump;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    private void StartMovement(Vector2 direction)
    {
        _isMoving = true;
        _movingDirection = direction;
    }

    private void StopMovement()
    {
        _isMoving = false;
        Rb.velocity = new Vector2(0f, Rb.velocity.y);
    }

    private void Move()
    {
        Rb.AddRelativeForce(_movingDirection * _movementForce);
        Rb.velocity = new Vector2(Mathf.Clamp(Rb.velocity.x, -_maxVelocity.x, _maxVelocity.x), Rb.velocity.y);
    }

    private void Jump()
    {
        Rb.AddRelativeForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
