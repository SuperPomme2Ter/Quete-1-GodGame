using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color colour;
    public Vector2 uvPos;
}

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode
    {
        NoiseMap,
        ColourMap,
        Mesh
    }

    public enum SeedType
    {
        Fixed,
        Random
    }

    public DrawMode drawMode;

    

    public const int mapChunkSize = 200;

    [Range(1, 20)] public int numberOfChunks = 1;
    public Noise.NormalizeMode normalizeMode;
    public SeedType seedType;


    public float noiseScale;

    public int octaves;
    [Range(0, 1)] public float persistance;
    public float lacunarity;


    [Range(0, 6)] public int LoD = 0;
    
    public bool fallofMap = false;

    public float fallofForce;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public float MeshOffset = 1;

    public AnimationCurve meshHeightCurve;
    
    public Texture2D solTest;

    private List<List<MeshData>> aaaa;

    
    
    
    
    

    public void GenerateMap()
    {
        //int totalChunks = numberOfChunks * numberOfChunks;
        int mapTotalSize = mapChunkSize * numberOfChunks;
        if (SeedType.Random == seedType)
        {
            seed=Random.Range(0, int.MaxValue);
        }
        float[,] noiseMap = Noise.GenerateNoiseMap(mapTotalSize, mapTotalSize, seed, noiseScale, octaves, persistance,
            lacunarity, offset, normalizeMode);

        Color[] colourMap = new Color[mapTotalSize * mapTotalSize];
        for (int y = 0; y < mapTotalSize; y++)
        {
            for (int x = 0; x < mapTotalSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapTotalSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (fallofMap)
        {
            float[,] newFallofMap=FallofMap.GenerateFallofMap(mapTotalSize, mapTotalSize,fallofForce);
            for (int y = 0; y < mapTotalSize; y++)
            {
                for (int x = 0; x < mapTotalSize; x++)
                {
                    noiseMap[x, y] =Mathf.Clamp01(newFallofMap[x,y]-noiseMap[x,y]);
                }
            }
        }
        if (drawMode == DrawMode.NoiseMap)
        {
            //display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            //display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {

            int indexRefX = 0;
            int indexRefY = 0;
            aaaa = new List<List<MeshData>>();
            for (int y = 0; y < numberOfChunks; y++)
            {
                aaaa.Add(new List<MeshData>());
                for (int x = 0; x < numberOfChunks; x++)
                {
                    aaaa[y].Add(MeshGenerator.GenerateTerrainMesh(mapChunkSize,regions,solTest,x, y, noiseMap, MeshOffset, meshHeightCurve, LoD));

                }
                //Debug.Log(indexRef);
                //Debug.Log(noiseMap.GetLength(0));

                // aaaa[i]=MeshGenerator.GenerateTerrainMesh(indexRef,noiseMap, MeshOffset, meshHeightCurve, LoD);
            }
            
            List<Texture2D> allTextures = new List<Texture2D>();
            for (int y = 0; y < numberOfChunks; y++)
            {
                for (int x = 0; x < numberOfChunks; x++)
                {
                    //allTextures.Add(TextureGenerator.TextureFromColourMap(regions,y,x,noiseMap,mapChunkSize, mapChunkSize));
                    allTextures.Add(solTest);
                }
            }


            //Texture2D refTexture = TextureGenerator.TextureFromColourMap(colourMap, mapTotalSize, mapTotalSize);
            List<Color[]> aaa=new List<Color[]>();

            int b = 0;
            
            
            display.DrawMesh(aaaa, allTextures,mapChunkSize);
        }

        

    }



    void OnValidate() {
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
    }

    public void CallClearChunks()
    {
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.ClearMesh();
    }
    
    

}
