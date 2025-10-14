using UnityEngine;

namespace TerrainGenerator
{
    [System.Serializable]
    public class BrushSettings
    {
        public float radius = 2.5f;
        public float strengthPerSecond = 3f;
        public AnimationCurve falloff = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}