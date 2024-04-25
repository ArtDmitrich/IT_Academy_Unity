using UnityEngine;

public class PingPongBehavior : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _movementSpeed;
    [Min(0.1f)]
    [SerializeField] private float _minDistanceToTarget;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private Vector3 _directionToMove;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        SetDirectionToMove();
    }

    private void Update()
    {
        if(CheckDistanceToTarget())
        {
            SetTargetPos();
            SetDirectionToMove();
        }

        transform.Translate(_directionToMove * _movementSpeed * Time.deltaTime);
    }

    private void SetDirectionToMove()
    {
        //вызываем только когда нужно изменить направление движения. расчеты "дороже"
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
        //можно написать любую логику выбора цели
        (_startPos, _targetPos) = (_targetPos, _startPos);
    }
}
