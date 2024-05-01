using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollViewPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.text = TextFileReader.GetTextFromTextFile(ToText.ScrollView);
    }
}
