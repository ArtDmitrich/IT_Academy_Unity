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

    public void Move(float horInput)
    {
         _rb.AddRelativeForce(0f, 0f, _movementForce * horInput, ForceMode.Force);        
    }

    public void Rotate(float verInput)
    {
        _rb.AddRelativeTorque(0f, verInput * _rotationForce, 0f, ForceMode.VelocityChange);
    }
}
