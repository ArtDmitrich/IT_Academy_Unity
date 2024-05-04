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
        if (_dropdown != null)
        {
            _dropdown.options.Clear();

            foreach (var dropItem in _dropItems)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData() { text = dropItem });
            }

            _dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(_dropdown); });
        }
        else
        {
            Debug.Log("Dropdown is NULL!!!");
        }
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

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(delegate { DropDownItemSelected(_dropdown); });
    }
}
