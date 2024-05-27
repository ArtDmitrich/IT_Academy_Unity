using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _rotationSpeed);
    }
}
