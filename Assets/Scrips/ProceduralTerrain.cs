using System;
using UnityEngine;

namespace TerrainGenerator
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class ProceduralTerrain : MonoBehaviour
    {

        [Header("Résolution (nb de segments)")]
        public int xSegments = 100;
        public int zSegments = 100;
        

        [Header("Contraintes")]
        public float minHeight = 0f;
        public float maxHeight = 20f;

        Mesh mesh;
        MeshCollider meshCollider;
        
        

        Vector3[] vertices;
        Vector2[] uvs;
        Color[] colors;
        int[] triangles;

        
        float[,] heights;
        
        
        
        

        // void SyncVerticesFromHeights()
        // {
        //     int vi = 0;
        //     for (int z = 0; z < ZCount; z++)
        //     {
        //         for (int x = 0; x < XCount; x++, vi++) // it just works
        //         {
        //             float h = heights[x, z];
        //             vertices[vi].y = h;
        //         }
        //     }
        // }

        void ApplyToMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mesh.colors = colors;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            meshCollider.sharedMesh = null;         
            meshCollider.sharedMesh = mesh;
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
    }
}