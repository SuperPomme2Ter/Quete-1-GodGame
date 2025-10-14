using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GetVerticesByCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera cam;
    public GameObject test;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    List<int> allTrianglesIndex=new List<int>();
    

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            allTrianglesIndex.Clear();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            
            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }
            allTrianglesIndex.Add(hit.triangleIndex);
            
            mesh=hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
            vertices=mesh.vertices;
            triangles=mesh.triangles;
            // int triangleIndex=hit.triangleIndex;
            // Debug.Log("hit.triangleIndex = " + triangleIndex);
            Debug.Log("Mesh : " + hit.collider.gameObject.name);
            int v0Index;
            int v1Index;
            int v2Index;
                


                    v0Index = triangles[hit.triangleIndex * 3];
                    v1Index = triangles[hit.triangleIndex * 3+1];
                    v2Index = triangles[hit.triangleIndex * 3+2];

                    vertices[v0Index] += Vector3.up;
                    vertices[v1Index] += Vector3.up;
                    vertices[v2Index] += Vector3.up;

                mesh.vertices = vertices;
                mesh.RecalculateNormals();
                
        }

    }
    
}
// public void ModifyHeightAtWorld(Vector3 worldPoint, float delta, float radius, AnimationCurve falloff = null)
// {
//     
//     Vector3 local = transform.InverseTransformPoint(worldPoint);
//     float x0 = -0.5f;
//     float z0 = -0.5f;
//
//     
//     int minX = Mathf.Max(0, Mathf.FloorToInt((local.x - radius - x0) / CellSizeX));
//     int maxX = Mathf.Min(XCount - 1, Mathf.CeilToInt((local.x + radius - x0) / CellSizeX));
//     int minZ = Mathf.Max(0, Mathf.FloorToInt((local.z - radius - z0) / CellSizeZ));
//     int maxZ = Mathf.Min(ZCount - 1, Mathf.CeilToInt((local.z + radius - z0) / CellSizeZ));
//
//     for (int z = minZ; z <= maxZ; z++)
//     {
//         for (int x = minX; x <= maxX; x++)
//         {
//             float vx = x0 + x * CellSizeX;
//             float vz = z0 + z * CellSizeZ;
//             float dist = Vector2.Distance(new Vector2(local.x, local.z), new Vector2(vx, vz));
//             if (dist > radius) continue;
//
//             float t = 1f - Mathf.Clamp01(dist / radius);
//             float weight = falloff != null ? falloff.Evaluate(t) : t; // dégressif
//             heights[x, z] = Mathf.Clamp(heights[x, z] + delta * weight, minHeight, maxHeight);
//         }
//     }
//     
//
//     SyncVerticesFromHeights();
//     ApplyToMesh();
// }
// }