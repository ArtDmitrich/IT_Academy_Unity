using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;
    [SerializeField] private string _explosiveSoundName;

    private void Explosion ()
    {
        Hit();

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_power, explosionPos, _radius);
            }
        }

        var _explosiveSound = AudioManager.Instance.GetSound(_explosiveSoundName);

        if (_explosiveSound != null)
        {
            _explosiveSound.transform.position = transform.position;
            _explosiveSound.Play();
            Debug.LogWarning("Sound Explosion");
        }
    }

    private void OnCollisionEnter()
    {
        Explosion();
    }
}
