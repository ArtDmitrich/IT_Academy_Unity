using TMPro;
using UnityEngine;

public class PlayerCanvasController : MonoBehaviour, IHiddenText
{
    [SerializeField] private TMP_Text _hiddenText;
    
    public void HideText()
    {
        _hiddenText.gameObject.SetActive(false);
    }

    public void ShowText(string text)
    {
        _hiddenText.text = text;
        _hiddenText.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _hiddenText.gameObject.SetActive(false);
    }
}
