using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    private List<GameObject> instantiatedMesh=new List<GameObject>();

    [SerializeField]
    private GameObject meshParent;

    public void DrawTexture(Texture2D texture) {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
    }

    public void DrawMesh(List<List<MeshData>> allMesh, List<Texture2D> texture,int mapChunkSize)
    {
        ClearMesh();

        int a = 0;
        for (int y = 0; y < allMesh.Count; y++)
        {
            for (int x = 0; x < allMesh[y].Count; x++)
            {
                GameObject newMesh=Instantiate(meshRenderer.gameObject, new Vector3((mapChunkSize-1)*y,0,-(mapChunkSize-1)*x), Quaternion.identity,meshParent.transform);
                Mesh aaa = allMesh[x][y].CreateMesh();
                newMesh.GetComponent<MeshFilter>().mesh = aaa;
                newMesh.GetComponent<MeshRenderer>().material.mainTexture = texture[a];
                newMesh.GetComponent<MeshCollider>().sharedMesh = aaa;
                newMesh.name = "Mesh "+(allMesh[x].Count*y+x);
                newMesh.SetActive(true);
                instantiatedMesh.Add(newMesh);
                a++;
            }
        }

        List<NavMeshSurface> bbb = meshParent.GetComponents<NavMeshSurface>().ToList();
        foreach (NavMeshSurface go in bbb)
        {
            go.BuildNavMesh();
        }
        
        
        
    }

    public void ClearMesh()
    {
        for (int i = 0; i < instantiatedMesh.Count; i++)
        {
            DestroyImmediate(instantiatedMesh[i]);
        }
    }
    
    
}