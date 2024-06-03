using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<PrefabData> prefabs;
    [SerializeField] private GameObject _erroneousObject;

    public GameObject Spawn(string prefabName)
    {
        foreach (var item in prefabs)
        {
            if (item.Title == prefabName)
            {
                var prefab = item.Prefab;
                return Instantiate(prefab);
            }
        }

        return _erroneousObject;
    }
}
