using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class QuadGenerator : MonoBehaviour
{
    [Min(0.1f)]
    [SerializeField] private float _edgeLength;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        _mesh = GenerateQuad(Vector3.zero, 1f);

        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.RecalculateNormals();
    }
    public Mesh GenerateQuad(Vector3 meshCenter, float edgeLength)
    {
        var mesh = new Mesh();

        mesh.vertices = GenerateVertices(meshCenter, edgeLength);
        mesh.triangles = GenerateTriangles();

        return mesh;
    }

    private Vector3[] GenerateVertices(Vector3 meshCenter, float edgeLength)
    {
        var centerX = meshCenter.x;
        var centerY = meshCenter.y;
        var centerZ = meshCenter.z;
        
        var halfEdgeLength = edgeLength / 2f;

        return new Vector3[]
        {
            new Vector3(centerX - halfEdgeLength, centerY, centerZ - halfEdgeLength),
            new Vector3(centerX - halfEdgeLength, centerY, centerZ + halfEdgeLength),
            new Vector3(centerX + halfEdgeLength, centerY, centerZ + halfEdgeLength),
            new Vector3(centerX + halfEdgeLength, centerY, centerZ - halfEdgeLength),
        };
    }

    private int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 2, 2, 3, 0 };
    }
}
