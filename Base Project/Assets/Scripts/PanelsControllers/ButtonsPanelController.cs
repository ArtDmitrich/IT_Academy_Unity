using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanelController : MonoBehaviour
{
    [SerializeField] private List<Button> _numbers;
    [SerializeField] private Button _disable;

    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        RefreshPanel();
    }

    private void Start()
    {
        foreach (var button in _numbers)
        {
            if (button != null)
            {
                button.onClick.AddListener(delegate { ButtonClicked(button); });              
            }
            else
            {
                Debug.Log("Number button is NULL!!!");
            }
        }

        if (_disable != null)
        {
            _disable.onClick.AddListener(delegate { SetInteractableForAllButtons(false); });           
        }
        else
        {
            Debug.Log("Disable button is NULL!!!");
        }
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
        foreach (var button in _numbers)
        {
            button.interactable = value;
        }

        _disable.interactable = value;
    }

    private void RefreshPanel()
    {
        SetInteractableForAllButtons(true);

        ChangeText("New Text");
    }

    private void OnDestroy()
    {
        foreach (var button in _numbers)
        {
            button.onClick.RemoveListener(delegate { ButtonClicked(button); });
        }

        _disable.onClick.RemoveListener(delegate { SetInteractableForAllButtons(false); });
    }
}
