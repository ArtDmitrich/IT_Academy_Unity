using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TogglesPanelController : MonoBehaviour
{
    [SerializeField] private List<Toggle> _toggles;
    [SerializeField] private TMP_Text _text;

    private ToggleGroup _toggleGroup;

    private void OnEnable()
    {
        RefreshPanel();
    }

    private void Start()
    {
        _toggleGroup = GetComponent<ToggleGroup>();

        foreach (var toggle in _toggles)
        {
            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });

            if(_toggleGroup != null)
            {
                toggle.group = _toggleGroup;
            }
        }

        ChangeText("New Text");
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        ChangeText(toggle.name);
    }

    private void ChangeText(string newText)
    {
        _text.text = newText;
    }
    private void RefreshPanel()
    {
        ChangeText("New Text");
    }
}
