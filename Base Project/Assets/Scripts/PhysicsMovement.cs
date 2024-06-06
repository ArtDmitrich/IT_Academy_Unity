using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementForce;

    private Rigidbody2D Rb { get {  return _rb = _rb ?? GetComponent<Rigidbody2D>(); } }
    private Rigidbody2D _rb;

    public void Move(Vector2 direction)
    {
        Rb.velocity = direction * _movementForce;
    }
}
