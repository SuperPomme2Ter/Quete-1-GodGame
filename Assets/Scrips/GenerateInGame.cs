using UnityEngine;

public class GenerateInGame : MonoBehaviour
{
    [SerializeField]
    MapGenerator mapGenerator;
    void Start()
    {
        mapGenerator.CallClearChunks();
        mapGenerator.GenerateMap();
    }
    
}
