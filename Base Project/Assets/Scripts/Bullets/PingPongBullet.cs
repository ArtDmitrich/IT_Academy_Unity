using UnityEngine;

public class PingPongBullet : Bullet
{
    [SerializeField] private ParticleSystem _reboundEffect;
    [SerializeField] private string _reboundSoundName;


    private void OnCollisionEnter(Collision collision)
    {
        _reboundEffect.transform.position = collision.contacts[0].point;
        _reboundEffect.Play();

        var _reboundSound = AudioManager.Instance.GetSound(_reboundSoundName);

        if (_reboundSound != null)
        {
            _reboundSound.transform.position = transform.position;
            _reboundSound.Play();
        }
    }
}
