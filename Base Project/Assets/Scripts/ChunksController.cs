using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    public bool MoveRight;
    public float MovementSpeedMultiplier;

    [SerializeField] private List<Transform> _chunks;

    [SerializeField] private float _maxOffset;
    [SerializeField] private float _offsetSpeed;

    private void Update()
    {
        var _direction = MoveRight ? Vector3.right : Vector3.left;

        foreach (var chunk in _chunks)
        {
            chunk.localPosition += _offsetSpeed * Time.deltaTime * MovementSpeedMultiplier * _direction;

            if (chunk.localPosition.x <= -_maxOffset)
            {
                chunk.localPosition = Vector3.right * _maxOffset;
            }
            else if (chunk.localPosition.x >= _maxOffset)
            {
                chunk.localPosition = Vector3.left * _maxOffset;
            }
        }
    }
}
