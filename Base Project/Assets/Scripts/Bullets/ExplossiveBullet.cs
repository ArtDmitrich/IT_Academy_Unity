using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;
    [SerializeField] private ParticleSystem _explosion;

    private void Explosion ()
    {
        _explosion?.Play();

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

        Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter()
    {
        Explosion();
    }
}
