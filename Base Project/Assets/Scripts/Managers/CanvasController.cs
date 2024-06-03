using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public int Lives
    {
        set
        {
            _lives.text = "Lives: " + value;
        }
    }

    public int Score
    {
        set
        {
            _score.text = "Score: " + value;
        }
    }

    public Button Start;

    [SerializeField] private TMP_Text _lives;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _finalText;
    [SerializeField] private Image _background;

    private void OnEnable()
    {
        _background.gameObject.SetActive(true);
        Start.gameObject.SetActive(true);
        _finalText.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _lives.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _background.gameObject.SetActive(false);
        Start.gameObject.SetActive(false);

        _score.gameObject.SetActive(true);
        _lives.gameObject.SetActive(true);
    }

    public void FinishGame(bool playerWon)
    {
        _background.gameObject.SetActive(true);
        _finalText.gameObject.SetActive(true);

        _finalText.text = playerWon ? "Congratulations!" : "Game Over!";
    }
}
