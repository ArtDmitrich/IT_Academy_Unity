using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    public void RotatePlatform(float input)
    {
        transform.Rotate(0f, input * _rotationSpeed, 0f);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
