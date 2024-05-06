using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private Button _backToMainMenu;
    [SerializeField] private Button _buttons;
    [SerializeField] private Button _toggles;
    [SerializeField] private Button _drops;
    [SerializeField] private Button _input;
    [SerializeField] private Button _scrollView;

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
        AddListenerToButton(_backToMainMenu, delegate { ButtonClicked(ToText.MainMenu); });
        AddListenerToButton(_buttons, delegate { ButtonClicked(ToText.Buttons); });
        AddListenerToButton(_toggles, delegate { ButtonClicked(ToText.Toggles); });
        AddListenerToButton(_drops, delegate { ButtonClicked(ToText.Drops); });
        AddListenerToButton(_input, delegate { ButtonClicked(ToText.Input); });
        AddListenerToButton(_scrollView, delegate { ButtonClicked(ToText.ScrollView); });

        AddPanelToList(ToText.MainMenu, _mainMenuPanel);
        AddPanelToList(ToText.Buttons, _buttonsPanel);
        AddPanelToList(ToText.Toggles, _togglesPanel);
        AddPanelToList(ToText.Drops, _dropsPanel);
        AddPanelToList(ToText.Input, _inputPanel);
        AddPanelToList(ToText.ScrollView, _scrollViewPanel);

        _currentPanel = _mainMenuPanel;

        foreach (var panel in _panels)
        {
            panel.Value.SetActive(false);
        }
        
        _currentPanel.SetActive(true);
        _backToMainMenu.gameObject.SetActive(false);
        _titleText.text = ToText.MainMenu;
    }

    public void ButtonClicked(string buttonName)
    {
        _titleText.text = buttonName;

        _currentPanel.SetActive(false);
        _currentPanel = _panels.GetValueOrDefault(buttonName);
        _currentPanel.SetActive(true);

        if (_currentPanel != _mainMenuPanel)
        {
            _backToMainMenu.gameObject.SetActive(true);
        }
        else
        {
            _backToMainMenu.gameObject.SetActive(false);
        }
    }

    private void AddListenerToButton(Button button, UnityAction method)
    {
        if (button == null)
        {            
            Debug.Log("Button is NULL!!!");
            return;
        }

        button.onClick.AddListener(method);
    }

    private void AddPanelToList(string key, GameObject panel)
    {
        if (panel == null)
        {
            Debug.Log($"Panel {key} is NULL!!!");
            return;
        }

        _panels.Add(key, panel);
    }

    private void OnDestroy()
    {
        _backToMainMenu.onClick.RemoveListener(delegate { ButtonClicked(ToText.MainMenu); });
        _buttons.onClick.RemoveListener(delegate { ButtonClicked(ToText.Buttons); });
        _toggles.onClick.RemoveListener(delegate { ButtonClicked(ToText.Toggles); });
        _drops.onClick.RemoveListener(delegate { ButtonClicked(ToText.Drops); });
        _input.onClick.RemoveListener(delegate { ButtonClicked(ToText.Input); });
        _scrollView.onClick.RemoveListener(delegate { ButtonClicked(ToText.ScrollView); });
    }
}

