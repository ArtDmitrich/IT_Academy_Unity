using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] Transform _nextPatrolPoint;
    [SerializeField] string _enemyTag;

    private EnemyController _enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _enemyTag && collision.TryGetComponent(out _enemy))
        {
            _enemy.TargetToMove = _nextPatrolPoint;
        }
    }
}
