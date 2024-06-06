using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime * _movementSpeed);
    }
}
