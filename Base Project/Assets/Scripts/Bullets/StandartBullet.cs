using Unity.VisualScripting;
using UnityEngine;

public class StandartBullet : Bullet
{
    [SerializeField] private float _bulletForce;

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.contacts[0];
        _hitEffect.transform.position = contact.point;
        _hitEffect.transform.LookAt(contact.normal);

        collision.rigidbody.AddForce(transform.forward *  _bulletForce);

        Hit();
    }
}
