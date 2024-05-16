using UnityEngine;

//Add this component to BulletSpot on Gun
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _shootForce;

    public void Shoot(Bullet bullet)
    {
        var bulletRb = bullet.BulletRigidbody;

        if (bulletRb != null)
        {
            bulletRb.AddRelativeForce(transform.forward * _shootForce, ForceMode.VelocityChange);            
        }
    }
}
