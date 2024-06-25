using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _maxDistance;
    [SerializeField] private float _mediumDistance;
    [SerializeField] private float _attackDistance;

    [SerializeField] private float _maxUpdateTime;
    [SerializeField] private float _mediumUpdateTime;
    [SerializeField] private float _minUpdateTime;

    private NavMeshAgent Agent { get { return _agent = _agent ?? GetComponent<NavMeshAgent>(); } }
    private NavMeshAgent _agent;

    private Animator Animator { get { return _animator = _animator ?? GetComponent<Animator>(); } }
    private Animator _animator;

    private EnemyState _currentState;
    private float _sqrDistanceToTarget;

    private float _destinationTimer;
    private float _timeToUpdateDestination;

    private bool _isAttacking;
    private bool _isAlive;

    private float _sqrMaxDist;
    private float _sqrMedDist;
    private float _sqrAtcDist;

    private void Start()
    {
        _sqrMaxDist = _maxDistance * _maxDistance;
        _sqrMedDist = _mediumDistance * _mediumDistance;
        _sqrAtcDist = _attackDistance * _attackDistance;

        _isAlive = true;
    }

    private void Update()
    {
        if(_isAttacking || !_isAlive)
        {
            return;
        }

        SetState();
        ActionByState();
    }

    private void Idle()
    {
        //some idle logic
    }

    private void Move()
    {
        _destinationTimer -= Time.deltaTime;

        if (_destinationTimer <= 0f && Target != null)
        {
            _destinationTimer += _timeToUpdateDestination;

            Agent.SetDestination(Target.position);
            SetTimeToUpdateDestination();
        }
    }

    private void Attack()
    {
        _isAttacking = true;
        transform.LookAt(Target.position);
        Animator.SetTrigger("Attack");
    }

    private void AttackEnd()
    {
        _isAttacking = false;
    }

    private void SetTimeToUpdateDestination()
    {
        if (Target == null)
        {
            _timeToUpdateDestination = 0f;
            return;
        }

        if (_sqrDistanceToTarget >= _sqrMaxDist)
        {
            _timeToUpdateDestination = _maxUpdateTime;
        }
        else if (_sqrDistanceToTarget >= _sqrMedDist)
        {
            _timeToUpdateDestination = _mediumUpdateTime;
        }
        else 
        {
            _timeToUpdateDestination = _minUpdateTime;
        }
    }

    private void SetState()
    {
        if (Target == null)
        {
            _currentState = EnemyState.Idle;
            return;
        }

        _sqrDistanceToTarget = (Target.position - transform.position).sqrMagnitude;

        if (_sqrDistanceToTarget > _sqrAtcDist)
        {
            _currentState = EnemyState.Move;
        }
        else
        {
            _currentState = EnemyState.Attack;
        }
    }

    private void ActionByState()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                Idle();
                return;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                Idle();
                break;
        }
    }
}
