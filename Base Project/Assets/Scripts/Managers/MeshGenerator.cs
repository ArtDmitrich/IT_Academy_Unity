using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public Mesh Generate(Vector3 meshCenter, Vector3 size)
    {
        var mesh = new Mesh();

        mesh.vertices = GenerateVertices(meshCenter, size);
        mesh.triangles = GenerateTriangles();

        return mesh;
    }

    private Vector3[] GenerateVertices(Vector3 meshCenter, Vector3 size)
    {
        var centerX = meshCenter.x;
        var centerY = meshCenter.y;
        var centerZ = meshCenter.z;

        var halfSizeX = size.x / 2f;
        var halfSizeY = size.y / 2f;
        var halfSizeZ = size.z / 2f;


        return new Vector3[]
        {
            new Vector3(centerX - halfSizeX, centerY + halfSizeY, centerZ - halfSizeZ), //0: -1, 1, -1
            new Vector3(centerX - halfSizeX, centerY + halfSizeY, centerZ + halfSizeZ), //1: -1, 1, 1
            new Vector3(centerX + halfSizeX, centerY + halfSizeY, centerZ + halfSizeZ), //2: 1, 1, 1
            new Vector3(centerX + halfSizeX, centerY + halfSizeY, centerZ - halfSizeZ), //3: 1, 1, -1

            new Vector3(centerX - halfSizeX, centerY - halfSizeY, centerZ - halfSizeZ), //4: -1, -1, -1
            new Vector3(centerX - halfSizeX, centerY - halfSizeY, centerZ + halfSizeZ), //5: -1, -1, 1
            new Vector3(centerX + halfSizeX, centerY - halfSizeY, centerZ + halfSizeZ), //6: 1, -1, 1
            new Vector3(centerX + halfSizeX, centerY - halfSizeY, centerZ - halfSizeZ), //7: 1, -1, -1
        };
    }

    private int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 2, 0, 2, 3, 5, 1, 0, 5, 0, 4, 4, 0, 3, 4, 3, 7, 7, 3, 2, 7, 2, 6, 6, 2, 1, 6, 1, 5, 5, 4, 7, 5, 7, 6 };
    }
}
