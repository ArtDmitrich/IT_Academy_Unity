using UnityEngine;

public class PingPongBullet : Bullet
{
    [SerializeField] private ParticleSystem _reboundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        _reboundEffect.transform.position = collision.contacts[0].point;
        _reboundEffect.Play();
    }

    private void OnDisable()
    {        
        _hitEffect.Play();
    }
}
