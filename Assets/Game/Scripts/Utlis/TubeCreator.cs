using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TubeCreator
{
    private const int _sides = 10;

    public static Mesh GenerateMesh(List<Vector3> meshPoints, Transform obj, float tubeRadius)
    {
        Mesh _mesh = new Mesh();
        _mesh.name = "instance";
        var verticesLength = _sides * meshPoints.Count;
        Vector3[] vertices = Array.Empty<Vector3>();

        if (_mesh == null || meshPoints.Count <= 1)
        {
            return null;
        }

        if (vertices.Length != verticesLength)
        {
            vertices = new Vector3[verticesLength];

            var indices = GenerateIndices(meshPoints);
            var uvs = GenerateUVs(meshPoints);

            if (verticesLength > _mesh.vertexCount)
            {
                _mesh.vertices = vertices;
                _mesh.triangles = indices;
                _mesh.uv = uvs;
            }
            else
            {
                _mesh.triangles = indices;
                _mesh.vertices = vertices;
                _mesh.uv = uvs;
            }
        }

        var currentVertIndex = 0;

        for (int i = 0; i < meshPoints.Count; i++)
        {
            var circle = CalculateCircle(i, meshPoints, tubeRadius, obj);
            foreach (var vertex in circle)
            {
                vertices[currentVertIndex++] = /*vertex;*/ obj.InverseTransformPoint(vertex);
            }
        }

        _mesh.vertices = vertices;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _mesh = MakeDoubleSided(_mesh);
        return _mesh;
    }


    public static Mesh MakeDoubleSided(Mesh mesh)
    {
        // duplicate all triangles with inverted normals so the mesh
        // can be seen both from the outside and the inside
        List<int> newTriangles = new List<int>(mesh.triangles);

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            int vertIdx1 = mesh.triangles[i];
            int vertIdx2 = mesh.triangles[i + 1];
            int vertIdx3 = mesh.triangles[i + 2];

            newTriangles.Add(vertIdx3);
            newTriangles.Add(vertIdx2);
            newTriangles.Add(vertIdx1);
        }

        mesh.SetTriangles(newTriangles, 0);

        return mesh;
    }


    private static Vector2[] GenerateUVs(List<Vector3> meshPoints)
    {
        var uvs = new Vector2[meshPoints.Count * _sides];

        for (int segment = 0; segment < meshPoints.Count; segment++)
        {
            for (int side = 0; side < _sides; side++)
            {
                var vertIndex = (segment * _sides + side);
                var u = side / (_sides - 1f);
                var v = segment / (meshPoints.Count - 1f);

                uvs[vertIndex] = new Vector2(u, v);
            }
        }

        return uvs;
    }


    private static int[] GenerateIndices(List<Vector3> meshPoints)
    {
        // Two triangles and 3 vertices
        var indices = new int[meshPoints.Count * _sides * 2 * 3];

        var currentIndicesIndex = 0;
        for (int segment = 1; segment < meshPoints.Count; segment++)
        {
            for (int side = 0; side < _sides; side++)
            {
                var vertIndex = (segment * _sides + side);
                var prevVertIndex = vertIndex - _sides;

                // Triangle one
                indices[currentIndicesIndex++] = prevVertIndex;
                indices[currentIndicesIndex++] = (side == _sides - 1) ? (vertIndex - (_sides - 1)) : (vertIndex + 1);
                indices[currentIndicesIndex++] = vertIndex;


                // Triangle two
                indices[currentIndicesIndex++] =
                    (side == _sides - 1) ? (prevVertIndex - (_sides - 1)) : (prevVertIndex + 1);
                indices[currentIndicesIndex++] = (side == _sides - 1) ? (vertIndex - (_sides - 1)) : (vertIndex + 1);
                indices[currentIndicesIndex++] = prevVertIndex;
            }
        }

        return indices;
    }


    private static Vector3[] CalculateCircle(int index, List<Vector3> circlePoints, float radius, Transform obj)
    {
        var dirCount = 0;
        var forward = Vector3.zero;

        // If not first index
        if (index > 0)
        {
            forward += (circlePoints[index] - circlePoints[index - 1]).normalized;
            dirCount++;
        }

        // If not last index
        if (index < circlePoints.Count - 1)
        {
            forward += (circlePoints[index + 1] - circlePoints[index]).normalized;
            dirCount++;
        }

        // Forward is the average of the connecting edges directions
        forward = (forward / dirCount).normalized;
        var side = Vector3.Cross(forward, forward + new Vector3(.123564f, .34675f, .756892f)).normalized;
        var up = Vector3.Cross(forward, side).normalized;

        var circle = new Vector3[_sides];
        var angle = 0f;
        var angleStep = (2 * Mathf.PI) / _sides;

        var t = index / (circlePoints.Count - 1f);


        for (int i = 0; i < _sides; i++)
        {
            var x = Mathf.Cos(angle);
            var y = Mathf.Sin(angle);

            //  circle[i] = circlePoints[index] + side * x * radius + up * y * radius;
            circle[i] = obj.TransformPoint(circlePoints[index]) + side * x * radius + up * y * radius;

            angle += angleStep;
        }

        return circle;
    }
}