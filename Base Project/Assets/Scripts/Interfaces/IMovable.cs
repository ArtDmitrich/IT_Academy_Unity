using UnityEngine;

public interface IMovable
{
    public void StartMovement(Vector3 direction, float speed);
    public void StopMovement();
}
