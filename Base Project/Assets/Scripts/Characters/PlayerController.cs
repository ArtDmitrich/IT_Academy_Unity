using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    [Inject] private InputController _input;

    private void StartMovement(Vector2 direction)
    {
        Movement?.StartMovement(direction);
    }

    private void StopMovement()
    {
        Movement?.StopMovement();
    }

    private void OnEnable()
    {
        _input.PlayerMovementStarted += StartMovement;
        _input.PlayerMovementStoped += StopMovement;
    }

    private void OnDisable()
    {
        _input.PlayerMovementStarted -= StartMovement;
        _input.PlayerMovementStoped -= StopMovement;
    }
}
