using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanelController : MonoBehaviour
{
    [SerializeField] private List<Button> _numberButtons;
    [SerializeField] private Button _disableButton;

    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        RefreshPanel();
    }

    private void Start()
    {
        foreach (var button in _numberButtons)
        {
            button.onClick.AddListener(delegate { ButtonClicked(button); });
        }

        _disableButton.onClick.AddListener(delegate { SetInteractableForAllButtons(false); });
    }

    private void ButtonClicked(Button button)
    {
        ChangeText(button.name + ". Clicked");
    }

    private void ChangeText(string newText)
    {
        _text.text = newText;
    }

    private void SetInteractableForAllButtons(bool value)
    {
        foreach (var button in _numberButtons)
        {
            button.interactable = value;
        }

        _disableButton.interactable = value;
    }

    private void RefreshPanel()
    {
        SetInteractableForAllButtons(true);

        ChangeText("New Text");
    }
}
