using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabList;

    private GameObject _instante;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnPrefab(_prefabList[Random.Range(0, _prefabList.Count)]);
        }
    }
    private void SpawnPrefab(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is NULL!!");
            return;
        }

        if (_instante != null)
        {
            Destroy(_instante);
        }

        var position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        var rotation = Quaternion.identity;

        _instante = Instantiate(prefab, position, rotation);
    }
}
