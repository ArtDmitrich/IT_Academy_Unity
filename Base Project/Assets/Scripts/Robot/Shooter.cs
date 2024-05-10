using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _shootForce;

    public void Shoot(Bullet bullet)
    {
        var bulletRb = bullet.GetRigidbody();

        if (bulletRb != null)
        {
            bulletRb.AddRelativeForce(transform.forward * _shootForce, ForceMode.VelocityChange);
        }
    }
}
