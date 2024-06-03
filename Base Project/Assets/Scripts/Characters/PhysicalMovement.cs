using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicalMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _maxVelocity;

    [SerializeField] private float _movementForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _distanceToCheckGround;

    [SerializeField] private Transform _characterLowPoint;

    private Rigidbody2D Rb { get { return _rb = _rb ?? GetComponent<Rigidbody2D>(); } }
    private Rigidbody2D _rb;

    private bool _isMoving;
    private Vector2 _movingDirection;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    public void StartMovement(Vector2 direction)
    {
        _isMoving = true;
        _movingDirection = direction;
    }

    public void StopMovement()
    {
        _isMoving = false;

        var velocityY = Mathf.Clamp(Rb.velocity.y, -_maxVelocity.y, _maxVelocity.y);
        Rb.velocity = new Vector2(0f, velocityY);
    }

    public void Jump()
    {
        if (CheckGrounding())
        {
            Rb.AddRelativeForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        Rb.AddRelativeForce(_movingDirection * _movementForce);

        var velocityX = Mathf.Clamp(Rb.velocity.x, -_maxVelocity.x, _maxVelocity.x);
        Rb.velocity = new Vector2(velocityX, Rb.velocity.y);        
    }

    private bool CheckGrounding()
    {
        var hit = Physics2D.Raycast(_characterLowPoint.position, Vector2.down, _distanceToCheckGround);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
