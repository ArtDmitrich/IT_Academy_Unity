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

    private int _indexSelectedItem;

    private void Start()
    {
        AddListenerToButton(_nextItem, delegate { SelectItem(LoopedIndexes.GetNextIndex(_indexSelectedItem, _Items.Count), true); });
        AddListenerToButton(_prevItem, delegate { SelectItem(LoopedIndexes.GetPrevIndex(_indexSelectedItem, _Items.Count), false); });

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

    private void OnDestroy()
    {
        _nextItem.onClick.RemoveAllListeners();
        _prevItem.onClick.RemoveAllListeners();
    }
}
