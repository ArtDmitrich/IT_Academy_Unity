using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private string _openningName;
    [SerializeField] private string _endingName;


    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform _playerSpot;
    [SerializeField] private TimelinesController _timelinesController;

    [Inject] private CanvasController _canvas;
    [Inject] private ObstaclesController _obstacles;
    [Inject] private InputController _input;

    public void OpenningEnd()
    {
        _player.SetActiveForMovement(true);
    }   

    public void EndingEnd()
    {
        _canvas.SetActiveMainMenu(true);
    }

    private void StartGame()
    {
        _canvas.SetActiveMainMenu(false);

        _obstacles.RefreshObstacles();

        _player.Respawn(_playerSpot.position);
        _player.SetActiveForMovement(false);

        _timelinesController.Play(_openningName);
    }

    private void EndGame()
    {
        _timelinesController.Play(_endingName);
    }

    private void Start()
    {
        _canvas.SetActiveMainMenu(true);
    }

    private void OnEnable()
    {
        _input.Enable();
        _player.PlayerDied += EndGame;
        _canvas.StartButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _input.Disable();
        _player.PlayerDied -= EndGame;
        _canvas.StartButton.onClick.RemoveListener(StartGame);
    }
}
