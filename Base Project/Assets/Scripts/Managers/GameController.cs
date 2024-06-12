using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _maxDisatanceToSpawn;
    [SerializeField] private float _minDisatanceToSpawn;
    [SerializeField] private Transform _player;
    [SerializeField] private List<WavesData> _wavesDatas;

    //[Inject] private InputController _input;
    [Inject] private Pool _pool;

    private Vector2[] _directions = new [] { Vector2.down, Vector2.right, Vector2.left, Vector2.up};
    private List<Enemy> _enemies;

    private void Update()
    {
        //Calling method for debug
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StartWave(_wavesDatas[0]));
        }
    }

    private IEnumerator StartWave(WavesData waveData)
    {
        foreach (var chunk in waveData.chunksOfWaves)
        {
            for (var i = 0; i < chunk.Count; i++)
            {
                var item = _pool.GetPooledItem(chunk.PrefabName);
                MovableEnemy enemy;

                if (item != null && item.TryGetComponent(out enemy))
                {
                    //some logic to variable spot for spawn enemy
                    var newPos = _directions[Random.Range(0, _directions.Length)] * Random.Range(_minDisatanceToSpawn, _maxDisatanceToSpawn);
                    enemy.transform.position = _player.transform.position + new Vector3(newPos.x, newPos.y, _player.transform.position.z);

                    enemy.Target = _player;

                    enemy.EnemyDead += EnemyDead;
                    _enemies.Add(enemy);
                }
            }

            yield return new WaitForSeconds(chunk.TimeToNextChunk);
        }
    }

    private void EnemyDead(Enemy enemy)
    {
        if (enemy == null)
        { 
            return;
        }

        enemy.EnemyDead -= EnemyDead;
        enemy.gameObject.SetActive(false);

        _enemies.Remove(enemy);
    }
}
