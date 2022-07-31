using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class QuadGenerator : MonoBehaviour
{
    [SerializeField]
    private int _xSize, _ySize = 1;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "My Quad";
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateShape()
    {
        GenerateVertices();
        CreateTriangles();

        UpdateMesh();
    }
    private void GenerateVertices()
    {
        vertices = new Vector3[(_xSize + 1) * (_ySize + 1)];
        for (int i = 0, y = 0; y < _ySize; y++)
        {
            for (int x = 0; x < _xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }

        }
    }
    private void CreateTriangles()
    {
        triangles = new int[_xSize * _ySize * 6];
        for(int ti = 0, vi = 0, y = 0; y < _ySize; y++, vi++)
        {
            for(int x = 0; x < _xSize; x++,ti += 6,vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + _xSize + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + _xSize + 2;
            }
        }

    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if(vertices == null)
        {
            return;
        }
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.02f);
        }
    }
}
