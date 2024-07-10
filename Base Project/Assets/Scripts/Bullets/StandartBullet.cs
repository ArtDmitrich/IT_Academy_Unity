using Unity.VisualScripting;
using UnityEngine;

public class StandartBullet : Bullet
{
    [SerializeField] private float _bulletForce;
    [SerializeField] private string _hitSoundName;

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.contacts[0];
        _hitEffect.transform.position = contact.point;
        _hitEffect.transform.LookAt(contact.normal);

        collision.rigidbody.AddForce(transform.forward *  _bulletForce);

        Hit();

        var _hitSound = AudioManager.Instance.GetSound(_hitSoundName);

        if (_hitSound != null)
        {
            _hitSound.transform.position = transform.position;
            _hitSound.Play();
            Debug.LogWarning("Sound Hit");
        }
    }
}
