using UnityEngine;

namespace TerrainGenerator
{
    // public class RaiseLowerBrush : MonoBehaviour
    // {
    //     [SerializeField] ProceduralTerrain terrain;
    //     [SerializeField] BrushSettings brush = new BrushSettings();
    //
    //     public void BuildAt(Vector3 worldPoint)
    //     {
    //         float delta = brush.strengthPerSecond * Time.deltaTime;
    //         terrain.ModifyHeightAtWorld(worldPoint, +delta, brush.radius, brush.falloff);
    //     }
    //
    //     public void DestroyAt(Vector3 worldPoint)
    //     {
    //         float delta = brush.strengthPerSecond * Time.deltaTime;
    //         terrain.ModifyHeightAtWorld(worldPoint, -delta, brush.radius, brush.falloff);
    //     }
    //
    //     // Accès pour l’Input (ex : changer rayon à la molette)
    //     public BrushSettings settings => brush;
    // }
}