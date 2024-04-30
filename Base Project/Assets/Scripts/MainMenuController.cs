using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private Button _backToMainMenuButton;
    [SerializeField] private Button _buttonsButton;
    [SerializeField] private Button _togglesButton;
    [SerializeField] private Button _dropsButton;
    [SerializeField] private Button _inputButton;
    [SerializeField] private Button _scrollViewButton;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private GameObject _togglesPanel;
    [SerializeField] private GameObject _dropsPanel;
    [SerializeField] private GameObject _inputPanel;
    [SerializeField] private GameObject _scrollViewPanel;

    private Dictionary<string, GameObject> _panels = new Dictionary<string, GameObject>();
    private GameObject _currentPanel;

    private void Start()
    {
        _backToMainMenuButton.onClick.AddListener(delegate { ButtonClicked(ToText.MainMenu); });
        _buttonsButton.onClick.AddListener(delegate { ButtonClicked(ToText.Buttons); });
        _togglesButton.onClick.AddListener(delegate { ButtonClicked(ToText.Toggles); });
        _dropsButton.onClick.AddListener(delegate { ButtonClicked(ToText.Drops); });
        _inputButton.onClick.AddListener(delegate { ButtonClicked(ToText.Input); });
        _scrollViewButton.onClick.AddListener(delegate { ButtonClicked(ToText.ScrollView); });

        _panels.Add(ToText.MainMenu, _mainMenuPanel);
        _panels.Add(ToText.Buttons, _buttonsPanel);
        _panels.Add(ToText.Toggles, _togglesPanel);
        _panels.Add(ToText.Drops, _dropsPanel);
        _panels.Add(ToText.Input, _inputPanel);
        _panels.Add(ToText.ScrollView, _scrollViewPanel);

        _currentPanel = _mainMenuPanel;

        foreach (var panel in _panels)
        {
            panel.Value?.SetActive(false);
        }
        
        _currentPanel?.SetActive(true);
        _backToMainMenuButton.gameObject?.SetActive(false);
        _titleText.text = ToText.MainMenu;
    }

    public void ButtonClicked(string buttonName)
    {
        _titleText.text = buttonName;

        _currentPanel?.SetActive(false);
        _currentPanel = _panels.GetValueOrDefault(buttonName);
        _currentPanel?.SetActive(true);

        if (_currentPanel != _mainMenuPanel)
        {
            _backToMainMenuButton.gameObject?.SetActive(true);
        }
        else
        {
            _backToMainMenuButton.gameObject?.SetActive(false);
        }
    }
}

