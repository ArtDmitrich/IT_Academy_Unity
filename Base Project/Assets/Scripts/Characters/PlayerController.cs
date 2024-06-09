using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

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
        InputController.Instance.PlayerMovementStarted += StartMovement;
        InputController.Instance.PlayerMovementStoped += StopMovement;
    }

    private void OnDisable()
    {
        InputController.Instance.PlayerMovementStarted -= StartMovement;
        InputController.Instance.PlayerMovementStoped -= StopMovement;
    }
}
