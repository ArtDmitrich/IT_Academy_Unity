using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodiesController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _elements;

    public void SetIsKinematic(bool isKinematic)
    {
        foreach (var element in _elements)
        {
            element.isKinematic = isKinematic;
        }
    }
}
