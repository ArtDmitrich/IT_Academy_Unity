using UnityEngine;

public class ScalerBehavior : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _scalingTime;
    [SerializeField] private Vector3 _targetScale;


    private Vector3 _startScale;
    private float _elapsedTime;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.localScale = Vector3.Lerp(_startScale, _targetScale, _elapsedTime / _scalingTime);

        if(_elapsedTime >= _scalingTime)
        {
            _elapsedTime = 0;
            SetTargetScale();
        }       
    }

    private void SetTargetScale()
    {
        //можно написать любую логику выбора целевого масштаба
        (_startScale, _targetScale) = (_targetScale, _startScale);
    }
}
