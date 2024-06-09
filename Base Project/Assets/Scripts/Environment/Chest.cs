using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IUsable
{
    [SerializeField] private Sprite _chestOpened;
    [SerializeField] private Sprite _chestClosed;

    [SerializeField] private string _textForPlayer;

    private SpriteRenderer _renderer;
    private IHiddenText _hiddenText;
    private bool _isOpened;

    public void Use()
    {
        SetSprite(_chestOpened);
        _isOpened = true;
    }

    private void Start()
    {
        _isOpened = false;
        _renderer = GetComponentInChildren<SpriteRenderer>();

        SetSprite(_chestClosed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOpened && collision.TryGetComponent(out _hiddenText))
        {
            _hiddenText.ShowText(_textForPlayer);
        }
    }

    private void OnTriggerExit2D()
    {
        if (_hiddenText != null)
        {
            _hiddenText.HideText();
            _hiddenText = null;
        }
    }

    private void SetSprite(Sprite sprite)
    {
        if (_renderer == null)
        {
            return;
        }

        _renderer.sprite = sprite;
    }
}
