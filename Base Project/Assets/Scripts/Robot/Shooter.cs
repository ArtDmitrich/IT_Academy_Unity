using UnityEngine;

//Add this component to BulletSpot on Gun
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _shootForce;
    [SerializeField] private ParticleSystem _muzzleFlash;

    public void Shoot(Bullet bullet)
    {
        _muzzleFlash.Play();
        var bulletRb = bullet.BulletRigidbody;

        if (bulletRb != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bulletRb.velocity = Vector3.zero;
            bulletRb.AddRelativeForce(transform.forward * _shootForce, ForceMode.VelocityChange);
        }
    }
}
