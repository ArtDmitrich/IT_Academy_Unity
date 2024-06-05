using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<ChangerData> _prefabs;

    public ChangerController Spawn(string prefabName)
    {
        foreach (var item in _prefabs)
        {
            if (item.Title == prefabName)
            {
                var prefab = item.Prefab;
                return Instantiate(prefab);
            }
        }

        return null;
    }
}
