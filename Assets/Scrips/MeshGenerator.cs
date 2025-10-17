using Unity.VisualScripting;
using UnityEngine;

public class MeshData {
    public Vector3[] vertices; // nombre de lignes
    public int[] triangles; // ensemble de 3 vertices, formant le triangle en question
    public Vector2[] uvs;

    int triangleIndex; // utilisé pour crée les triangles sans faire de index out of range et repasser sur des triangles déjà crées
    public Mesh mesh;
    public float[] verticePathValue;

    public MeshData(int meshWidth, int meshHeight) {
        vertices = new Vector3[meshWidth * meshHeight]; // a savoir que la width et height et la taille du plane
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
        verticePathValue = new float[meshWidth * meshHeight];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles [triangleIndex] = a;
        triangles [triangleIndex + 1] = b;
        triangles [triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals ();
        return mesh;
    }
    
    
    void ApplyToMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
    }
    
    // public void ModifyHeightAtWorld(Vector3 worldPoint, float delta, float radius,GameObject meshObject, AnimationCurve falloff = null)
    // {
    //         
    //     Vector3 local = meshObject.transform.InverseTransformPoint(worldPoint);
    //     float x0 = -0.5f;
    //     float z0 = -0.5f;
    //
    //         
    //     int minX = Mathf.Max(0, Mathf.FloorToInt((local.x - radius - x0) ));
    //     int maxX = Mathf.Min(XCount - 1, Mathf.CeilToInt((local.x + radius - x0) ));
    //     int minZ = Mathf.Max(0, Mathf.FloorToInt((local.z - radius - z0) ));
    //     int maxZ = Mathf.Min(ZCount - 1, Mathf.CeilToInt((local.z + radius - z0) ));
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

}

public static class MeshGenerator {

    
    
    public static MeshData GenerateTerrainMesh(int size,int xIndex,int yIndex,float[,] heightMap,float terrainOffset,AnimationCurve heightCurve, int LoD) {
        // Permet de centrer le modèle
        float topLeftX = (size - 1) / 2f;
        float topLeftZ = (size - 1) / 2f;

        int meshSimplificationIncrement =(LoD==0)?  1:LoD*2;
        int verticesPerLine=(size-1)/meshSimplificationIncrement+1;

        MeshData meshData = new MeshData (verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < size; y+=meshSimplificationIncrement) {
            for (int x = 0; x < size; x+=meshSimplificationIncrement) {// saut de vertices pour correspondre au LoD

                meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x,heightCurve.Evaluate((heightMap [x+
                    ((size-1)*xIndex), y+((size-1)*yIndex)])) * terrainOffset, topLeftZ - y);
                meshData.verticePathValue[vertexIndex] = heightMap[x + ((size-1) * xIndex), y + ((size-1) * yIndex)];
                meshData.uvs [vertexIndex] = new Vector2 (x / (float)size, y / (float)size);

                if (x < (size-1) && y < (size-1)) {
                    meshData.AddTriangle (vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle (vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    } 
    public static MeshData GenerateTerrainMesh(int size,TerrainType[] regions, Texture2D solTexture,int xIndex,int yIndex,float[,] heightMap,float terrainOffset,AnimationCurve heightCurve, int LoD) {
        // Permet de centrer le modèle
        float topLeftX = (size - 1) / 2f;
        float topLeftZ = (size - 1) / 2f;

        int meshSimplificationIncrement =(LoD==0)?  1:LoD*2;
        int verticesPerLine=(size-1)/meshSimplificationIncrement+1;

        MeshData meshData = new MeshData (verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < size; y+=meshSimplificationIncrement) {
            for (int x = 0; x < size; x+=meshSimplificationIncrement) {// saut de vertices pour correspondre au LoD

                meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x,heightCurve.Evaluate((heightMap [x+
                    ((size-1)*xIndex), y+((size-1)*yIndex)])) * terrainOffset, topLeftZ - y);
                
                meshData.verticePathValue[vertexIndex] = heightMap[x + ((size-1) * xIndex), y + ((size-1) * yIndex)];
                
                //meshData.uvs [vertexIndex] = new Vector2 (x / (float)size, y / (float)size);
                float lastRegionValue = 0;
                foreach (var region in regions)
                {
                    
                    //heightMap [x+(xIndex*width), y+(yIndex*height)
                    if ((heightMap [x+ ((size-1)*xIndex), y+((size-1)*yIndex)]) 
                        <= region.height)
                    {
                        float rslt = (region.height - lastRegionValue);
                        float coeff = (1 / rslt);
                        float valueByRegion = ((heightMap[x + ((size - 1) * xIndex), y + ((size - 1) * yIndex)])-lastRegionValue)*coeff;
                        valueByRegion *=2;
                        valueByRegion=valueByRegion-Mathf.Floor(valueByRegion);
                        //float aaa=region.height-lastRegionValue;
                        
                        
                        Vector2 m=new Vector2 (valueByRegion/2+region.uvPos.x,valueByRegion/2+region.uvPos.y);
                        // Vector2 ddddd = new Vector2(
                        //         ((heightMap [x+ (size-1)*xIndex, y+(size-1)*yIndex]-lastRegionValue)/region.height)/2+region.uvPos.x,
                        //     ((heightMap [x+ (size-1)*xIndex, y+(size-1)*yIndex]-lastRegionValue)/region.height)/2+region.uvPos.y);
                        //
                        // //ddddd += region.uvPos;
                        m=new Vector2(m.x-Mathf.Floor(m.x), m.y-Mathf.Floor(m.y));
                        
                        meshData.uvs[vertexIndex] = m;
                        // if (lastRegionValue > region.height)
                        // {
                        // lastRegionValue = region.height;
                        // }
                        break;
                    }
                    lastRegionValue = region.height;
                }

                if (x < (size-1) && y < (size-1)) {
                    meshData.AddTriangle (vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle (vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    }
    

}