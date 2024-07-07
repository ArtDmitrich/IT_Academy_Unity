using UnityEngine;

//Add this component to BulletSpot on Gun
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _shootForce;
    [SerializeField] private ParticleSystem _muzzleFlash;

    public void Shoot(Bullet bullet)
    {
        _muzzleFlash.Play();
        bullet.PreparingToShoot(transform);
        var bulletRb = bullet.BulletRigidbody;

        if (bulletRb != null)
        {           
            bulletRb.AddForce(transform.forward * _shootForce, ForceMode.VelocityChange);
        }
    }
}
