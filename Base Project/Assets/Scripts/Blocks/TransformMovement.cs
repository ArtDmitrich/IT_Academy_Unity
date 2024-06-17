using UnityEngine;

public class TransformMovement : MonoBehaviour, IMovable
{
    private float _movementSpeed;
    private bool _isMoving;
    private Vector3 _direction;

    public void StartMovement(Vector3 direction, float speed)
    {
        _isMoving = true;
        _direction = direction;
        _movementSpeed = speed;
    }

    public void StopMovement()
    {
        _isMoving = false;
    }

    private void Move()
    {
        transform.Translate(_direction * Time.deltaTime * _movementSpeed);
    }

    private void Update()
    {
        if(_isMoving)
        {
            Move();
        }
    }
}
