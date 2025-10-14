using UnityEngine;

public class FallofMap
{
    // int totalChunks = numberOfChunks * numberOfChunks;
    // int mapTotalSize = mapChunkSize * numberOfChunks;
    // float[,] noiseMap = Noise.GenerateNoiseMap(mapTotalSize, mapTotalSize, seed, noiseScale, octaves, persistance,
    //     lacunarity, offset, normalizeMode);
    //
    // Color[] colourMap = new Color[mapTotalSize * mapTotalSize];
    //     for (int y = 0; y < mapTotalSize; y++)
    // {
    //     for (int x = 0; x < mapTotalSize; x++)
    //     {
    //         float currentHeight = noiseMap[x, y];
    //         for (int i = 0; i < regions.Length; i++)
    //         {
    //             if (currentHeight <= regions[i].height)
    //             {
    //                 colourMap[y * mapTotalSize + x] = regions[i].colour;
    //                 break;
    //             }
    //         }
    //     }
    // }

    public static float[,] GenerateFallofMap(int width, int height, float force)
    {
        float[,] fallofMap= new float[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float x = i / (float)height * 2 - 1;
                float y = j / (float)width * 2 - 1;

                float value = Mathf.Max (Mathf.Abs (x), Mathf.Abs (y));
                value = force * value;
                fallofMap [i, j] = 1-value;
            }
        }
        return fallofMap;
    }
    
    
//     float[,] noiseMap = new float[mapWidth,mapHeight];
//     Debug.Log(mapWidth);
//     Debug.Log(mapHeight);
//     System.Random rndValue = new System.Random (seed);
//     Vector2[] octaveOffsets = new Vector2[octaves];
//         
//     float maxPossibleHeight = 0;
//     //float minPossibleHeight = 0;
//         
//     float amplitude = 1;
//     float frequency = 1;
//         
//         for (int i = 0; i < octaves; i++) {
//         float offsetX = rndValue.Next (-100000, 100000) + offset.x;
//         float offsetY = rndValue.Next (-100000, 100000) - offset.y;
//         octaveOffsets [i] = new Vector2 (offsetX, offsetY);
//
//         maxPossibleHeight += amplitude;
//         amplitude *= persistance;
//     }
//
// if (scale <= 0) {
//     scale = 0.0001f;
// }
//
// float maxLocalNoiseHeight = float.MinValue;
// float minLocalNoiseHeight = float.MaxValue;
//
// // Permet que le paramètre noise provoque un zoom au centre de la carte
// float halfWidth = mapWidth / 2f;
// float halfHeight = mapHeight / 2f;

}
