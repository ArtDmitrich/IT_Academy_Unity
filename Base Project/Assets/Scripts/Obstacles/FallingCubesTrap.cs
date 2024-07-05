using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubesTrap : Obstacle
{
    [SerializeField] private string _tagToCheck;
    [SerializeField] private RigidbodiesController _controller;

    public override void Refresh()
    {
        _controller.SetIsKinematic(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCheck))
        {
            _controller.SetIsKinematic(false);
        }
    }
}
