using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private Transform _targetToHorRotation;
    [SerializeField] private Transform _targetToVerRotation;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _clampDegrees;

    private float _rotationX;

    public void Rotate(Vector2 inputRotation)
    {
        HorizontalRotate(inputRotation.x);
        VerticalRotate(-inputRotation.y);
    }

    private void HorizontalRotate(float inputRotation)
    {
        _targetToHorRotation.Rotate(0f, inputRotation * _rotationSpeed, 0f);
    }

    private void VerticalRotate(float inputRotation)
    {
        _rotationX += inputRotation;

        _rotationX = Mathf.Clamp(_rotationX, -_clampDegrees, _clampDegrees);
        _targetToVerRotation.localEulerAngles = new Vector3(_rotationX, 0f, 0f);
    }

}
