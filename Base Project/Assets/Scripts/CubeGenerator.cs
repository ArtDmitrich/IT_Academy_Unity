using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeGenerator : MonoBehaviour
{
    [Min(0.1f)]
    [SerializeField] private float _edgeLength;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        _mesh = GenerateCube(Vector3.zero, 1f);

        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.RecalculateNormals();
    }

    private Mesh GenerateCube(Vector3 meshCenter, float edgeLength)
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
            new Vector3(centerX - halfEdgeLength, centerY + halfEdgeLength, centerZ - halfEdgeLength), //0: -1, 1, -1
            new Vector3(centerX - halfEdgeLength, centerY + halfEdgeLength, centerZ + halfEdgeLength), //1: -1, 1, 1
            new Vector3(centerX + halfEdgeLength, centerY + halfEdgeLength, centerZ + halfEdgeLength), //2: 1, 1, 1
            new Vector3(centerX + halfEdgeLength, centerY + halfEdgeLength, centerZ - halfEdgeLength), //3: 1, 1, -1

            new Vector3(centerX - halfEdgeLength, centerY - halfEdgeLength, centerZ - halfEdgeLength), //4: -1, -1, -1
            new Vector3(centerX - halfEdgeLength, centerY - halfEdgeLength, centerZ + halfEdgeLength), //5: -1, -1, 1
            new Vector3(centerX + halfEdgeLength, centerY - halfEdgeLength, centerZ + halfEdgeLength), //6: 1, -1, 1
            new Vector3(centerX + halfEdgeLength, centerY - halfEdgeLength, centerZ - halfEdgeLength), //7: 1, -1, -1
        };
    }

    private int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 2, 0, 2, 3, 5, 1, 0, 5, 0, 4, 4, 0, 3, 4, 3, 7, 7, 3, 2, 7, 2, 6, 6, 2, 1, 6, 1, 5, 5, 4, 7, 5, 7, 6 };
    }
}
