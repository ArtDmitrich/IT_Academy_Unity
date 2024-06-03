using UnityEngine;

public enum EnemyState
{
    Idle,
    WalkToTarget
}

[RequireComponent(typeof(PhysicalMovement))]
public class EnemyController : MonoBehaviour
{
    public Transform TargetToMove
    {
        get { return _targetToMove; }
        set
        {
            _targetToMove = value;
            _currentState = EnemyState.WalkToTarget;
            StartMovement(SetDirectionToMove());
        }
    }

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _minDistanceToTarget;

    private PhysicalMovement EnemyMovement { get { return _enemyMovement = _enemyMovement ?? GetComponent<PhysicalMovement>(); } }
    private PhysicalMovement _enemyMovement;

    private Transform _targetToMove;
    private Vector2 _currentDirection;
    private EnemyState _currentState;

    private void OnEnable()
    {
        _currentDirection = Vector2.right;
        _spriteRenderer.flipX = true;
    }

    private void Update()
    {
        if (TargetToMove == null || CheckMinDistanceToTarget())
        {
            SetActionByState(EnemyState.Idle);
            return;
        }

        SetActionByState(EnemyState.WalkToTarget);
    }

    private void SetActionByState(EnemyState state)
    {
        if (_currentState == state)
        {
            return;
        }

        switch (state)
        {
            case EnemyState.Idle:
                _currentState = state;
                StopMovement();
                break;
            case EnemyState.WalkToTarget:
                _currentState = state;
                StartMovement(SetDirectionToMove());
                break;
            default:
                StopMovement();
                break;
        }
    }

    private bool CheckMinDistanceToTarget()
    {
        var vectorToTarget = TargetToMove.position - transform.position;
        var sqrDistanceTotarget = vectorToTarget.sqrMagnitude;

        if (sqrDistanceTotarget <= _minDistanceToTarget * _minDistanceToTarget)
        {
            return true;
        }

        return false;
    }

    private Vector2 SetDirectionToMove()
    {
        if (TargetToMove == null)
        {
            return transform.position;
        }

        var vectorToTarget = TargetToMove.position - transform.position;
        return vectorToTarget.normalized;
    }

    private void StartMovement(Vector2 direction)
    {
        EnemyMovement.StartMovement(direction);
        _animator.SetTrigger("StartMovement");

        if (direction != _currentDirection)
        {
            _currentDirection = direction;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void StopMovement()
    {
        EnemyMovement.StopMovement();
        _animator.SetTrigger("StopMovement");
    }
}
    
