using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public UnityAction<GameObject, bool> ItemSelected;

    [SerializeField] private List<GameObject> _Items;

    [SerializeField] private Button _nextItem;
    [SerializeField] private Button _prevItem;

    [SerializeField] private float _durationOfDisable;

    private int _indexSelectedItem;

    private void Start()
    {
        AddListenerToButton(_nextItem, delegate { SelectItem(LoopedIndexes.GetNextIndex(_indexSelectedItem, _Items.Count), true); });
        AddListenerToButton(_prevItem, delegate { SelectItem(LoopedIndexes.GetPrevIndex(_indexSelectedItem, _Items.Count), false); });

        AddListenerToButton(_nextItem, delegate { StartCoroutine(Coroutines.SetButtonInteractivityForTime(_nextItem, false, _durationOfDisable)); });
        AddListenerToButton(_prevItem, delegate { StartCoroutine(Coroutines.SetButtonInteractivityForTime(_prevItem, false, _durationOfDisable)); });

        SelectItem(0, true);
    }

    private void AddListenerToButton(Button button, UnityAction method)
    {
        if (button == null)
        {
            Debug.LogError(gameObject.name + ": Button is NULL!!!");
            return;
        }

        button.onClick.AddListener(method);
    }

    private void SelectItem(int index, bool next)
    {
        _indexSelectedItem = index;
        ItemSelected?.Invoke(_Items[index], next);
    }

    private void RemoveListener(Button button, UnityAction method)
    {
        if (button == null)
        {
            return;
        }

        button.onClick.RemoveListener(method);
    }

    private void OnDestroy()
    {
        RemoveListener(_nextItem, delegate { SelectItem(LoopedIndexes.GetNextIndex(_indexSelectedItem, _Items.Count), true); });
        RemoveListener(_prevItem, delegate { SelectItem(LoopedIndexes.GetPrevIndex(_indexSelectedItem, _Items.Count), false); });

        RemoveListener(_nextItem, delegate { StartCoroutine(Coroutines.SetButtonInteractivityForTime(_nextItem, false, _durationOfDisable)); });
        RemoveListener(_prevItem, delegate { StartCoroutine(Coroutines.SetButtonInteractivityForTime(_prevItem, false, _durationOfDisable)); });
    }
}
