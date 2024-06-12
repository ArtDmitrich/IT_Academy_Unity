using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableEnemy : Enemy
{
    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            _directionToMove = GetDirectionToMove();
            Movement.StartMovement(_directionToMove);
        }
    }

    protected Vector2 _directionToMove;

    protected IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    private Transform _target;

    protected Vector2 GetDirectionToMove()
    {
        var vectorToTarget = Target.position - transform.position;
        return vectorToTarget.normalized;
    }
}
