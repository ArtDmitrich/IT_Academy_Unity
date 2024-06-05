using UnityEngine;

public class PhysicalMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody Rb { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }
    private Rigidbody _rb;

    public void StartMovement(Vector3 direction)
    {
        Rb.velocity = direction * _movementSpeed;
    }
}
