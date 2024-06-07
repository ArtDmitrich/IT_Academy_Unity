using UnityEngine;

public class Teleporter : MonoBehaviour, IUsable
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private string _tagToDetected;

    [SerializeField] private string _textForPlayer;

    private Transform _objForTeleportation;
    private IHiddenText _hiddenText;

    public void Use()
    {
        if (_objForTeleportation != null)
        {
            _objForTeleportation.position = _targetPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tagToDetected))
        {
            _objForTeleportation = collision.transform;

            if (_objForTeleportation.TryGetComponent(out _hiddenText))
            {
                _hiddenText.ShowText(_textForPlayer);
            }
        }
    }

    private void OnTriggerExit2D()
    {
        _objForTeleportation = null;

        if (_hiddenText != null)
        {
            _hiddenText.HideText();
            _hiddenText = null;
        }
    }
}
