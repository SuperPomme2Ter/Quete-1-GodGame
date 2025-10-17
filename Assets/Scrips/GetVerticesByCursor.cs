using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class GetVerticesByCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera cam;
    public MapGenerator regRef;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    
    List<int> allTrianglesIndex=new List<int>();

    
    

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AnimationCurve jjjj=regRef.meshHeightCurve;
            for(int k = 0; k < regRef.meshHeightCurve.keys.Length; k++)
            {
                regRef.meshHeightCurve.MoveKey(k,new Keyframe(regRef.meshHeightCurve.keys[k].time*regRef.MeshOffset,regRef.meshHeightCurve.keys[k].value*regRef.MeshOffset));
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
           
            
            if (!Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,1<<6))
            {
                return;
            }

            RaycastHit firstHit = hit;
            print(firstHit.point.y);
            RaycastHit[] hits=Physics.SphereCastAll(ray, 10,Mathf.Infinity,1<<6);
            for (int i = 0; i < hits.Length; i++)
            {
                hit=hits[i];
                mesh=hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                vertices=mesh.vertices;
                triangles=mesh.triangles;
                // int triangleIndex=hit.triangleIndex;
                // Debug.Log("hit.triangleIndex = " + triangleIndex);
                //Debug.Log("Mesh : " + hit.collider.gameObject.name);
                int v0Index;
                int v1Index;
                int v2Index;
                Vector3 local = hit.collider.transform.InverseTransformPoint(firstHit.point);
                MeshRenderer aaa = hit.collider.gameObject.GetComponent<MeshRenderer>();
                Texture2D bbb=(Texture2D)aaa.material.mainTexture;
                Color[] colours=new Color[vertices.Length];
                colours = bbb.GetPixels();
                for (int j = 0; j < vertices.Length; j++)
                {
                    
                    if (Vector3.Distance(new Vector3(vertices[j].x,0,vertices[j].z), new Vector3(local.x,0,local.z)) < 10)
                    {
                        if (vertices[j].y + 1 < regRef.MeshOffset)
                        {
                            vertices[j] += Vector3.up*(Time.deltaTime*10);
                        }
                        
                        
                        //bbb=(Texture2D)aaa.material.mainTexture;
                        
                        
                        
                        for (int k = 0; k < regRef.regions.Length; k++)
                        {
                            if (vertices[j].y/regRef.MeshOffset<=regRef.regions[k].height)
                            {
                                //bbb.SetPixel((int)vertices[j].x,(int)vertices[j].z,regRef.regions[k].colour);
                                colours[j] = regRef.regions[k].colour;
                                break;
                                //[x+(xIndex*241)
                            }
                        }
                        
                        
                        
                            
                        
                        //bbb.SetPixel(vertices[j].x, vertices[j].y, );

                    }
                    
                }
                bbb.SetPixels(colours);
                bbb.Apply();
                //aaa.material.mainTexture = bbb;
                
                
                mesh.vertices = vertices;
                //mesh.colors=colours;
                mesh.RecalculateNormals();
                
            }
            
            // for (int i = 0; i < 10; i++)
            // {
                // v0Index = triangles[hit.triangleIndex * 3];
                // v1Index = triangles[hit.triangleIndex * 3]+1;
                // v2Index = triangles[hit.triangleIndex * 3]+2;
                //
                //
                // vertices[v0Index] += Vector3.up;
                // vertices[v1Index] += Vector3.up;
                // vertices[v2Index] += Vector3.up;
            //}

                
                
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