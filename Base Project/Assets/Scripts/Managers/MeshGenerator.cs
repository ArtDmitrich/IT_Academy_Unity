using UnityEngine;

public static class MeshGenerator
{
    public static Mesh Generate(Vector3 size, Vector3 center = new Vector3())
    {
        var mesh = new Mesh();

        mesh.vertices = GenerateVertices(size, center);
        mesh.triangles = GenerateTriangles();

        return mesh;
    }

    private static Vector3[] GenerateVertices(Vector3 size, Vector3 center)
    {
        var halfSizeX = size.x / 2f;
        var halfSizeY = size.y / 2f;
        var halfSizeZ = size.z / 2f;


        return new Vector3[]
        {
            new Vector3(center.x - halfSizeX, center.y + halfSizeY, center.z - halfSizeZ), //0: -1, 1, -1
            new Vector3(center.x - halfSizeX, center.y + halfSizeY, center.z + halfSizeZ), //1: -1, 1, 1
            new Vector3(center.x + halfSizeX, center.y + halfSizeY, center.z + halfSizeZ), //2: 1, 1, 1
            new Vector3(center.x + halfSizeX, center.y + halfSizeY, center.z - halfSizeZ), //3: 1, 1, -1

            new Vector3(center.x - halfSizeX, center.y - halfSizeY, center.z - halfSizeZ), //4: -1, -1, -1
            new Vector3(center.x - halfSizeX, center.y - halfSizeY, center.z + halfSizeZ), //5: -1, -1, 1
            new Vector3(center.x + halfSizeX, center.y - halfSizeY, center.z + halfSizeZ), //6: 1, -1, 1
            new Vector3(center.x + halfSizeX, center.y - halfSizeY, center.z - halfSizeZ), //7: 1, -1, -1
        };
    }

    private static int[] GenerateTriangles()
    {
        return new int[] { 0, 1, 2, 0, 2, 3, 5, 1, 0, 5, 0, 4, 4, 0, 3, 4, 3, 7, 7, 3, 2, 7, 2, 6, 6, 2, 1, 6, 1, 5, 5, 4, 7, 5, 7, 6 };
    }
}
