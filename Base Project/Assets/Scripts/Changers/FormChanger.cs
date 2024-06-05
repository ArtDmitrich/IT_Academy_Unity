using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChanger : MonoBehaviour, IMutableObject
{
    [SerializeField] private List<Mesh> _meshes;
    private MeshFilter MeshFilter { get { return _meshFilter = _meshFilter ?? GetComponentInChildren<MeshFilter>(); } }
    private MeshFilter _meshFilter;

    public void ChangeProperty()
    {
        SetForm(_meshes[Random.Range(0, _meshes.Count)]);
    }

    private void SetForm(Mesh newForm)
    {
        MeshFilter.mesh = newForm;
    }
}
