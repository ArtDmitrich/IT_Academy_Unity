using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _movementSpeed;
    [Min(0.1f)]
    [SerializeField] private float _minDistanceToTarget;
    [SerializeField] private Transform _targetPos;

    private Vector3 _startPos;
    private Vector3 _directionToMove;

    private void Start()
    {
        _startPos = transform.position;
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
        var vectorToTarget = _targetPos.position - transform.position;
        _directionToMove = vectorToTarget.normalized;
    }

    private bool CheckDistanceToTarget()
    {
        var vectorToTarget = _targetPos.position - transform.position;
        var sqrDistanceTotarget = vectorToTarget.sqrMagnitude;

        if (sqrDistanceTotarget <= _minDistanceToTarget * _minDistanceToTarget)
        {
            return true;
        }

        return false;
    }

    private void SetTargetPos()
    {
        (_startPos, _targetPos.position ) = (_targetPos.position, _startPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
