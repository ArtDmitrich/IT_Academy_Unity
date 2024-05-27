using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _minLightIntensity;
    [SerializeField] private float _maxLightIntensity;
    [Min(1f)]
    [SerializeField] private float _lightIntnsityChangeSpeed;

    private int _pointer = 1;

    private Light Light { get {  return _light = _light ?? GetComponent<Light>(); } }
    private Light _light;

    private void Start()
    {
        Light.type = LightType.Point;
        Light.intensity = _minLightIntensity;
    }

    private void Update()
    {
        Light.intensity += _pointer * Time.deltaTime * _lightIntnsityChangeSpeed;

        var intencity = Light.intensity;

        if (intencity >= _maxLightIntensity || intencity <= _minLightIntensity)
        {
            _pointer *= -1;
        }
    }
}
