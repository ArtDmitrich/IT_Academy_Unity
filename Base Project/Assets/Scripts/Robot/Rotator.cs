using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    public void Rotate(float input)
    {
        transform.Rotate(Vector3.up * input);
    }
}
