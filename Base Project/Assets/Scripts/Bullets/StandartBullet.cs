using UnityEngine;

public class StandartBullet : Bullet
{
    private void OnCollisionEnter(Collision collision)
    {
        _bulletBody.SetActive(false);

        var contact = collision.contacts[0];
        _hitEffect.transform.position = contact.point;
        _hitEffect.transform.LookAt(contact.normal);
        _hitEffect.Play();
    }
}
