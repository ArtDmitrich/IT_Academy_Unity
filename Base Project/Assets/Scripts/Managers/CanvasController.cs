using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button StartGame;
    public Button RestartGame;
    public Button MainMenu;

    public TMP_Dropdown DifficultySelector;

    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private TMP_Text _finalText;
    [SerializeField] private Image _background;

    public void SetScore(int score)
    {
        _score.text = "Score: " + score;
    }

    public void SetSpeed(float speed)
    {
        _speed.text = "Speed: " + speed.ToString("0.0");
    }

    public void SetBestScore(int bestScore)
    {
        _bestScore.text = "Best: " + bestScore;
    }

    public void ActivateStartGameWindow()
    {
        StartGame.interactable = true;
        DifficultySelector.gameObject.SetActive(true);
        StartGame.gameObject.SetActive(true);
        _background.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }

    public void DeactivateStartGameWindow()
    {
        StartGame.interactable = false;
        DifficultySelector.gameObject.SetActive(false);
        StartGame.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void ActivateScoreAndSpeed(int score, float speed)
    {
        _score.gameObject.SetActive(true);
        _speed.gameObject.SetActive(true);
        SetScore(score);
        SetSpeed(speed);
    }

    public void ActivateFinalText(string text)
    {
        _finalText.gameObject.SetActive(true);
        _finalText.text = text;
        _background.gameObject.SetActive(true);
        RestartGame.gameObject.SetActive(true);
        RestartGame.interactable = true;
    }

    public void DeactivateFinalText()
    {
        _finalText.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _speed.gameObject.SetActive(false);
        RestartGame.interactable = false;
        RestartGame.gameObject.SetActive(false);
    }
}
