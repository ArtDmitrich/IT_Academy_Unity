using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _minDistanceToTarget;

    private IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    private float _sqrMinDistanceToTarget;
    private Vector2 _directionToMove;

    private void Start()
    {
        _sqrMinDistanceToTarget = _minDistanceToTarget * _minDistanceToTarget;
    }

    private void Update()
    {
        if (Target == null || CheckMinDistanceToTarget())
        {
            Movement.StopMovement();
            return;
        }

        var newDirection = GetDirectionToMove();
        if (newDirection != _directionToMove)
        {
            _directionToMove = newDirection;
            Movement.StartMovement(_directionToMove);
        }
    }

    private Vector2 GetDirectionToMove()
    {
        var vectorToTarget = Target.position - transform.position;
        return vectorToTarget.normalized;
    }

    private bool CheckMinDistanceToTarget()
    {
        var vectorToTarget = Target.position - transform.position;
        var sqrDistanceTotarget = vectorToTarget.sqrMagnitude;

        if (sqrDistanceTotarget <= _sqrMinDistanceToTarget)
        {
            return true;
        }

        return false;
    }
}
