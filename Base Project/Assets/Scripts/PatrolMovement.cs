using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetsToFly;

    [Min(0f)]
    [SerializeField] private float _movementSpeed;
    [Min(0.1f)]
    [SerializeField] private float _minDistanceToTarget;

    private Vector3 _targetPos;
    private Vector3 _directionToMove;

    private void Start()
    {
        SetTargetPos();
        SetDirectionToMove();
    }

    private void Update()
    {
        if (CheckDistanceToTarget())
        {
            SetTargetPos();
            SetDirectionToMove();
        }

        transform.Translate(_directionToMove * _movementSpeed * Time.deltaTime);
    }

    private void SetDirectionToMove()
    {
        var vectorToTarget = _targetPos - transform.position;
        _directionToMove = vectorToTarget.normalized;
    }

    private bool CheckDistanceToTarget()
    {
        var vectorToTarget = _targetPos - transform.position;
        var sqrDistanceTotarget = vectorToTarget.sqrMagnitude;

        if (sqrDistanceTotarget <= _minDistanceToTarget * _minDistanceToTarget)
        {
            return true;
        }

        return false;
    }

    private void SetTargetPos()
    {
        if (_targetsToFly.Count == 0)
        {
            _targetPos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        else
        {
            _targetPos = _targetsToFly[Random.Range(0, _targetsToFly.Count)].position;
        }
    }
}
