using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Button StartButton;

    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _info;

    public void SetActiveMainMenu(bool active)
    {
        StartButton.gameObject.SetActive(active);
        StartButton.interactable = active;
        _background.gameObject.SetActive(active);
        _info.gameObject.SetActive(active);
    }
}
