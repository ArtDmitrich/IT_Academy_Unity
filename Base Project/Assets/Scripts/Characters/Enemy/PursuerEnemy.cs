using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerEnemy : MovableEnemy
{
    [SerializeField] private float _minDistanceToTarget;

    private float _sqrMinDistanceToTarget;

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

    private bool CheckMinDistanceToTarget()
    {
        var vectorToTarget = Target.position - transform.position;
        var sqrDistanceTotarget = vectorToTarget.sqrMagnitude;

        return sqrDistanceTotarget <= _sqrMinDistanceToTarget;
    }
}
