using UnityEngine;

public class FallingBlock : MutableBlock
{
    private Rigidbody Rb { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }
    private Rigidbody _rb;

    public void ActivateGravity()
    {
        Rb.useGravity = true;
    }

    private void OnEnable()
    {
        Rb.useGravity = false;
        Rb.velocity = Vector3.zero;
    }
}
