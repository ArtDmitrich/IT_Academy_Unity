using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropsPanelController : MonoBehaviour
{
    [SerializeField] private List<string> _dropItems;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Dropdown _dropdown;

    private void OnEnable()
    {
        RefreshPanel();
    }

    private void Start()
    {
        _dropdown.options.Clear();

        foreach (var dropItem in _dropItems)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData() { text = dropItem });
        }

        DropDownItemSelected(_dropdown);
        _dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(_dropdown); });
    }

    private void DropDownItemSelected(TMP_Dropdown dropdown )
    {
        var index = dropdown.value;

        ChangeText(dropdown.options[index].text);
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
