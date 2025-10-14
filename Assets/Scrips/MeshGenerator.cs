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

}

public static class MeshGenerator {

    
    
    public static MeshData GenerateTerrainMesh(int xIndex,int yIndex,float[,] heightMap,float terrainOffset,AnimationCurve heightCurve, int LoD) {
        // Permet de centrer le modèle
        float topLeftX = (241 - 1) / 2f;
        float topLeftZ = (241 - 1) / 2f;

        int meshSimplificationIncrement =(LoD==0)?  1:LoD*2;
        int verticesPerLine=(241-1)/meshSimplificationIncrement+1;

        MeshData meshData = new MeshData (verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < 241; y+=meshSimplificationIncrement) {
            for (int x = 0; x < 241; x+=meshSimplificationIncrement) {// saut de vertices pour correspondre au LoD

                meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x,heightCurve.Evaluate((heightMap [x+
                    (240*xIndex), y+(240*yIndex)])) * terrainOffset, topLeftZ - y);
                //meshData.verticePathValue[vertexIndex] = heightMap[x + (240 * xIndex), y + (240 * yIndex)];
                meshData.uvs [vertexIndex] = new Vector2 (x / (float)241, y / (float)241);

                if (x < 241 - 1 && y < 241 - 1) {
                    meshData.AddTriangle (vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle (vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    }
    

}