using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysMover : MonoBehaviour
{
    [SerializeField] private float _movementForce;
    [SerializeField] private float _rotationForce;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(float horInput, float verInput)
    {
        if (horInput != 0)
        {
            _rb.AddRelativeForce(0f, 0f, _movementForce * horInput, ForceMode.Force);
        }

        if (verInput != 0)
        {
            _rb.AddRelativeTorque(0f, verInput * _rotationForce, 0f, ForceMode.VelocityChange);
        }
    }
}
