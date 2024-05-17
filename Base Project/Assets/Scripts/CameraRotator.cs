using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _clampDegrees;

    private float _rotationX;

    public void VerticalRotate(float inputRotation)
    {
        _rotationX += inputRotation;

        _rotationX = Mathf.Clamp(_rotationX, -_clampDegrees, _clampDegrees);
        transform.localEulerAngles = new Vector3(_rotationX, 0f, 0f);
    }
}
