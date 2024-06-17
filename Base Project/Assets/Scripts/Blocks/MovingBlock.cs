using UnityEngine;

public class MovingBlock : MutableBlock
{
    private IMovable BlockMovement { get { return _blockMovement = _blockMovement ?? GetComponent<IMovable>(); } }
    private IMovable _blockMovement;

    public void StartMovement(Vector3 direction, float speed)
    {
        BlockMovement.StartMovement(direction, speed);
    }

    public void StopMovement()
    {
        BlockMovement.StopMovement();
    }
}
