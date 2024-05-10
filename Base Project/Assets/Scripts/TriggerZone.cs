using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private BulletType _bulletType;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<RobotController>(out var robot))
        {
            robot.SetBulletType(_bulletType);
            robot.SetPosibilityOfShooting(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.TryGetComponent<RobotController>(out var robot))
        {
            robot.SetPosibilityOfShooting(false);
        }
    }
}
