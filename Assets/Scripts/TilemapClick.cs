using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClick : MonoBehaviour
{
    public TileBase TileToSet;
    public TileBase TileToChangeSW;
    public TileBase TileToChangeNW;
    private Vector3Int TilePositionNW1;
    private Vector3Int TilePositionNW2;
    private Vector3Int TilePositionSW1;
    private Vector3Int TilePositionSW2;
    private Tilemap tilemap;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        TilePositionNW1 = new Vector3Int(-3, 0, 0);
        TilePositionNW2 = new Vector3Int(2, -1, 0);
        TilePositionSW1 = new Vector3Int(0, 2, 0);
        TilePositionSW2 = new Vector3Int(-1, -3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = tilemap.WorldToCell(clickWorldPosition);
            if (clickCellPosition==TilePositionNW1)
            {
                if (tilemap.GetTile(clickCellPosition) == TileToChangeNW)
                {
                    tilemap.SetTile(clickCellPosition, TileToSet);
                }
                else
                {
                    tilemap.SetTile(clickCellPosition, TileToChangeNW);
                }
            }
            if (clickCellPosition == TilePositionNW2)
            {
                if (tilemap.GetTile(clickCellPosition) == TileToChangeNW)
                {
                    tilemap.SetTile(clickCellPosition, TileToSet);
                }
                else
                {
                    tilemap.SetTile(clickCellPosition, TileToChangeNW);
                }
            }
            if (clickCellPosition == TilePositionSW1)
            {
                if (tilemap.GetTile(clickCellPosition) == TileToChangeSW)
                {
                    tilemap.SetTile(clickCellPosition, TileToSet);
                }
                else
                {
                    tilemap.SetTile(clickCellPosition, TileToChangeSW);
                }
            }
            if (clickCellPosition == TilePositionSW2)
            {
                if (tilemap.GetTile(clickCellPosition) == TileToChangeSW)
                {
                    tilemap.SetTile(clickCellPosition, TileToSet);
                }
                else
                {
                    tilemap.SetTile(clickCellPosition, TileToChangeSW);
                }
            }

            //Debug.Log(clickCellPosition);
            //Debug.Log(tilemap.GetTile(clickCellPosition));

        }
    }
}
