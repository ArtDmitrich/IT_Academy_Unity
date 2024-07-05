using UnityEngine;

public class KillingObstacle : Obstacle
{
    [SerializeField] private string _tagToCheck;

    private void OnCollisionEnter(Collision collision)
    {
        var go = collision.gameObject;

        if (go.CompareTag(_tagToCheck))
        {
            var player = go.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.Death();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(_tagToCheck))
    //    {
    //        var player = other.GetComponentInParent<PlayerController>();
    //        if (player != null)
    //        {
    //            player.Death();
    //        }
    //    }
    //}
}
