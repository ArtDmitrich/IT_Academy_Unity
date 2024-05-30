using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    public bool IsMoveRight
    {
        get => _isMoveRight;
        set
        {
            if (value == _isMoveRight)
            {
                return;
            }

            _isMoveRight = value;

            _direction = _isMoveRight ? Vector3.right : Vector3.left;
        }
    }
    public float MovementSpeedMultiplier;

    [SerializeField] private List<Transform> _chunks;

    [SerializeField] private float _maxOffset;
    [SerializeField] private float _offsetSpeed;

    private Vector3 _direction;
    private bool _isMoveRight;

    private void Start()
    {
        _direction = Vector3.left;
    }

    private void Update()
    {
        foreach (var chunk in _chunks)
        {
            chunk.localPosition += _offsetSpeed * Time.deltaTime * MovementSpeedMultiplier * _direction;

            if (chunk.localPosition.x <= -_maxOffset)
            {
                chunk.localPosition += Vector3.right * 2 * _maxOffset;
            }
            else if (chunk.localPosition.x >= _maxOffset)
            {
                chunk.localPosition += Vector3.left* 2 * _maxOffset;
            }
        }
    }
}
