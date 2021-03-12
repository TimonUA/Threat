using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class Antenna : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3Int TilePosition1;
    private Vector3Int TilePosition2;
    public TileBase TileToChangeSE;
    public TileBase TileToChangeSW;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        TilePosition1 = new Vector3Int(-3, 4, 0);
        TilePosition2 = new Vector3Int(4, -4, 0);
        InvokeRepeating("ChangeTextureInFirst", 3f,4f);
        InvokeRepeating("ChangeTextureInSecond", 4f,4f);
    }

    // Update is called once per frame
    void ChangeTextureInFirst()
    {
        if (!PauseMenu.IsPaused && !Game.IsEnd)
        {
            if (tilemap.GetTile(TilePosition1) == TileToChangeSW)
                tilemap.SetTile(TilePosition1, TileToChangeSE);
            else
                tilemap.SetTile(TilePosition1, TileToChangeSW);
        }
       
    }
    void ChangeTextureInSecond()
    {
        if (!PauseMenu.IsPaused && !Game.IsEnd)
        {
            if (tilemap.GetTile(TilePosition2) == TileToChangeSE)
                tilemap.SetTile(TilePosition2, TileToChangeSW);
            else
                tilemap.SetTile(TilePosition2, TileToChangeSE);
        }
    }
}
