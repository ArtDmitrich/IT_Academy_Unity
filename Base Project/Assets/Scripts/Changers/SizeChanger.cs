using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour, IMutableObject
{
    [SerializeField] private int _minSize;
    [SerializeField] private int _maxSize;

    public void ChangeProperty()
    {
        SetSize(new Vector3(Random.Range(_minSize, _maxSize), 1, Random.Range(_minSize, _maxSize)));
    }

    private void SetSize(Vector3 newSize)
    {
        transform.localScale = newSize;
    }
}
