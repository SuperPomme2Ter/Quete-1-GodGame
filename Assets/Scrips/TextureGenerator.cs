using UnityEngine;

public static class TextureGenerator {

    public static Texture2D TextureFromColourMap(TerrainType[] region,int xIndex,int yIndex,float[,] heightMap, int width, int height) {
        Texture2D texture = new Texture2D (width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = heightMap [x+(xIndex*241), y+(yIndex*241)];
                for (int i = 0; i < region.Length; i++)
                {
                    if (currentHeight<=region[i].height)
                    {
                        colourMap[y * width + x] = region[i].colour;
                        break;
                    }
                }
                
            }
        }
        texture.SetPixels(colourMap);
        //texture.SetPixels (colourMap);
        texture.Apply ();
        return texture;
        
        // meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x,heightCurve.Evaluate((heightMap [x+
        //     (240*xIndex), y+(240*yIndex)])) * terrainOffset, topLeftZ - y);
        // meshData.uvs [vertexIndex] = new Vector2 (x / (float)241, y / (float)241);
        
        // for (int y = 0; y < mapTotalSize; y++)
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
    }


    // public static Texture2D TextureFromHeightMap(float[,] heightMap) {
    //     int width = heightMap.GetLength (0);
    //     int height = heightMap.GetLength (1);
    //
    //     Color[] colourMap = new Color[width * height];
    //     for (int y = 0; y < height; y++) {
    //         for (int x = 0; x < width; x++) {
    //             colourMap [y * width + x] = Color.Lerp (Color.black, Color.white, heightMap [x, y]);
    //         }
    //     }
    //
    //     return TextureFromColourMap (colourMap, width, height);
    // }

}