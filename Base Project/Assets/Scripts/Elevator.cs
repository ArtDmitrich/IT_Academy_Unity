using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _movingTime;

    private Vector3 _targetPos;
    private Vector3 _startPos;
    private float _elapsedTime;
    private bool _isMoving;

    public void SetOffsetY(float offsetY)
    {
        _startPos = transform.position;
        _targetPos += Vector3.up * offsetY;
        _isMoving = true;
    }

    public void Restart()
    {
        _startPos = Vector3.zero;
        transform.position = _startPos;
        _targetPos = _startPos;
        
        _isMoving = false;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(_startPos, _targetPos, _elapsedTime / _movingTime);

            if (_elapsedTime >= _movingTime)
            {
                _elapsedTime = 0;
                _isMoving = false;
            }
        }
    }
}
