using UnityEngine;
using UnityEngine.Events;

public class ResourceController : MonoBehaviour
{
    public int PlayerLives
    {
        get { return _playerLives; }
        set
        {
            _playerLives += value; 
            _canvas.Lives = _playerLives;

            if (_playerLives <= 0)
            {
                _playerLives = 0;
                PlayerDeaded?.Invoke();
            }
        }
    }

    public int CoinsCount
    {
        get { return _coinsCount; }
        set
        {
            _coinsCount += value;
            _canvas.Score = _coinsCount;
        }
    }

    public UnityAction PlayerDeaded;

    [SerializeField] private CanvasController _canvas;

    private int _playerLives;
    private int _coinsCount;
}
