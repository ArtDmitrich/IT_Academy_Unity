using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    public Rigidbody BulletRigidbody { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }

    private Rigidbody _rb;
}
