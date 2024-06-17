using UnityEngine;

public class MutableBlock : MonoBehaviour
{
    public Vector3 Size {  get; private set; }

    private MeshFilter MeshFilter { get { return _meshFilter = _meshFilter ?? GetComponent<MeshFilter>(); } }
    private MeshFilter _meshFilter;

    private BoxCollider Collider { get { return _collider = _collider ?? GetComponent<BoxCollider>(); } }
    private BoxCollider _collider;

    public void SetMesh(Mesh mesh)
    {
        MeshFilter.mesh = mesh;
        mesh.RecalculateNormals();        
    }

    public void SetColliderSize(Vector3 size)
    {
        Collider.size = size;
        Size = size;
    }
}
