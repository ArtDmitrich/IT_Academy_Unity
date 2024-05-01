using TMPro;
using UnityEngine;

public class InputPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _titleInput;
    [SerializeField] private TMP_InputField _textInput;

    private void OnEnable()
    {
        RefreshPanel();
    }

    private void RefreshPanel()
    {
        _titleInput.text = string.Empty;
        _textInput.text = string.Empty;
        _textInput.textComponent.enableWordWrapping = true;
    }
}
