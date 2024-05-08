using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private PhysMover _mover;

    [SerializeField] private Shooter _shooter;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private string _currentBullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentBullet != null)
            {
                _shooter.Shoot(_bulletSpawner.GetBullet(_currentBullet, _gunPos));
            }
        }
    }

    private void FixedUpdate()
    {
        var horInput = Input.GetAxis("Vertical");
        var verInput = Input.GetAxis("Horizontal");

        _mover.Move(horInput, verInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentBullet = other.tag;
    }

    private void OnTriggerExit(Collider other)
    { 
        _currentBullet = null; 
    }
}
