using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemies;
    [SerializeField] private PlayerController _player;

    private void Start()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Target = _player.transform;
        }
    }
}
