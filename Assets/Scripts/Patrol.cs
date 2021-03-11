using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class Patrol : MonoBehaviour
{
    private Vector3[] waypoints;
    private Vector3Int cellPosition;
    private Vector2 GateCoordinates;
    private bool IsPatrol;
    private int current;
    public float speed;
    public float TimeInCheck = 3f;
    private float downTime = 0.1f;
    private Camera mainCamera;
    private Game game;
    private Tilemap tilemap;
    private TileBase tile;
    private string FirstMovementStr;
    private string LastMovementStr;
    private string texture;
    // Start is called before the first frame update
    void Start()
    {
        FirstMovementStr = gameObject.GetComponent<Character>().FirstMovementSpriteStr;
        LastMovementStr = gameObject.GetComponent<Character>().LastMovementSpriteStr;
        texture = gameObject.GetComponent<Character>().mainSprite.name;
        FirstMovementStr = texture.Replace("SE", FirstMovementStr);
        LastMovementStr = texture.Replace("SE", LastMovementStr);
        waypoints = new Vector3[] { gameObject.GetComponent<Character>().startPosition, gameObject.GetComponent<Character>().gatePosition, gameObject.GetComponent<Character>().endPosition };
        current = 0;
        speed = 2;
        IsPatrol = true;
        mainCamera = Camera.main;
        game = mainCamera.GetComponent<Game>();
        tilemap = mainCamera.GetComponent<Game>().tilemap;
        tile = mainCamera.GetComponent<Game>().tile;
        GateCoordinates = gameObject.GetComponent<Character>().gatePosition;
        cellPosition = tilemap.WorldToCell(GateCoordinates);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            Patroling();
            if (TimeInCheck > 0 && downTime > 0)
            {
                if (transform.position == waypoints[2])
                {
                    IsPatrol = false;
                    TimeInCheck -= Time.deltaTime;
                }
                if (transform.position == waypoints[0] && tilemap.GetTile(cellPosition) == tile)
                {
                    downTime -= Time.deltaTime;
                }
                if (tilemap.GetTile(cellPosition) == tile && transform.position != waypoints[2] && transform.position != waypoints[0])
                {
                    LoadTexture(LastMovementStr);
                }
                else if (tilemap.GetTile(cellPosition) != tile)
                {
                    LoadTexture(FirstMovementStr);
                }
            }
            else
            {
                IsPatrol = true;
                if (transform.position == waypoints[0])
                {
                    IsPatrol = false;
                    LoadTexture();
                    Destroy(this);
                    game.Patrol();
                }
                else
                {
                    LoadTexture(LastMovementStr);
                }
            }
        }
    }
    void Patroling()
    {
        if (IsPatrol && tilemap.GetTile(cellPosition) != tile)
        {
            if (transform.position != waypoints[current])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
            else
                current = (current + 1) % waypoints.Length;
        }
        else if(IsPatrol && current!=2)
        {
            if (transform.position != waypoints[2] && transform.position != waypoints[1])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[0], speed * Time.deltaTime);
        }
    }
    void LoadTexture()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Character>().mainSprite;
    }
    void LoadTexture(string Texture)
    {
        AsyncOperationHandle<Sprite> spriteHandle = Addressables.LoadAssetAsync<Sprite>($"Assets/Sprites/Space/Characters/{Texture}.png");
        spriteHandle.Completed += LoadSpriteWhenReady;
    }
    void LoadSpriteWhenReady(AsyncOperationHandle<Sprite> handleToCheck)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = handleToCheck.Result;
        }
    }
}
