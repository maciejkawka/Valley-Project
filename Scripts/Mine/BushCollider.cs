using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCollider : MonoBehaviour
{


    GameObject Player;
    MeshCollider bushCollider;
    TerrainData data;
    Terrain gameTerrain;
    public GameObject bushCol;

    void Start()
    {
        gameTerrain = GetComponent<Terrain>();
        TerrainData data = gameTerrain.terrainData;
        float width = data.size.x;
        float height = data.size.z;
        float y = data.size.y;
        foreach (TreeInstance tree in data.treeInstances)
        {
            Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y + 1, tree.position.z * height);
            Instantiate(bushCol, position, Quaternion.identity);
        }
    }








}
