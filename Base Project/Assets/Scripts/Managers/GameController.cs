using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private string _bestScore;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _speedSizeIncrease;
    [SerializeField] private int _scoreToSpeedIncrease;

    [SerializeField] private Elevator _bloksParent;
    [SerializeField] private float _offsetY;

    [SerializeField] private Vector2[] _difficultyOptions;

    private int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            _canvas.SetScore(_score);

            if (_score % _scoreToSpeedIncrease == 0)
            {
                _blocksController.MovingBlockSpeed += _speedSizeIncrease;
                _canvas.SetSpeed(_blocksController.MovingBlockSpeed);
            }
        }
    }
    private int _score;

    private InputActions _inputActions;

    [Inject] private BlocksController _blocksController;
    [Inject] private CanvasController _canvas;

    private void Awake()
    {
        _inputActions = _inputActions ?? new InputActions();        
    }

    private void Start()
    {
        CallMainMenu();
    }

    private void CallMainMenu()
    {
        _inputActions.PlayerInput.Disable();

        var best = PlayerPrefs.GetInt(_bestScore);
        _canvas.SetBestScore(best);

        _canvas.ActivateStartGameWindow();
        _canvas.DeactivateFinalText();

        _bloksParent.Restart();
        _blocksController.Restart();
    }

    private void StartGame()
    {
        _score = 0;
        _canvas.DeactivateStartGameWindow();
        _canvas.DeactivateFinalText();
        _canvas.ActivateScoreAndSpeed(_score, _startSpeed);

        _blocksController.StartBlockSizeY = _offsetY;

        var indexDifficulty = _canvas.DifficultySelector.value;
        _blocksController.StartGame(_startSpeed, _difficultyOptions[indexDifficulty]);

        _inputActions.PlayerInput.Enable();
    }

    private void StopGame()
    {
        _inputActions.PlayerInput.Disable();
        _canvas.ActivateFinalText("YOU LOSE!");

        if (CheckBestScore(Score))
        {
            SetBestScore(Score);
        }
    }

    private void RestartGame()
    {
        _bloksParent.Restart();
        _blocksController.Restart();

        StartGame();
    }

    private void Tap_performed(InputAction.CallbackContext obj)
    {
        _blocksController.StopMovement();
        _bloksParent.SetOffsetY(-_offsetY);

        Score++;
    }

    private void SetBestScore(int score)
    {
        PlayerPrefs.SetInt(_bestScore, score);
        _canvas.SetBestScore(score);
    }

    private bool CheckBestScore(int currentScore)
    {
        var best = PlayerPrefs.GetInt(_bestScore);

        return best < currentScore;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.PlayerInput.Tap.performed += Tap_performed;

        _canvas.StartGame.onClick.AddListener(StartGame);
        _canvas.RestartGame.onClick.AddListener(RestartGame);
        _canvas.MainMenu.onClick.AddListener(CallMainMenu);

        _blocksController.MovingBlockDisappeared += StopGame;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.PlayerInput.Tap.performed -= Tap_performed;

        _canvas.StartGame.onClick.RemoveListener(StartGame);
        _canvas.RestartGame.onClick.RemoveListener(RestartGame);
        _canvas.MainMenu.onClick.RemoveListener(CallMainMenu);

        _blocksController.MovingBlockDisappeared += StopGame;
    }
}