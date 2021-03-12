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
    private int st;
    // Start is called before the first frame update
    void Start()
    {
        FirstMovementStr = gameObject.GetComponent<Character>().FirstMovementSpriteStr;
        LastMovementStr = gameObject.GetComponent<Character>().LastMovementSpriteStr;
        texture = gameObject.GetComponent<Character>().mainSprite.name;
        FirstMovementStr = texture.Replace("SE", FirstMovementStr);
        LastMovementStr = texture.Replace("SE", LastMovementStr);
        current = 0;
        speed = 2;
        st = 1;
        waypoints = new Vector3[] { gameObject.GetComponent<Character>().startPosition, gameObject.GetComponent<Character>().gatePosition, gameObject.GetComponent<Character>().endPosition };
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
            if (gameObject.tag != "MainInfected")
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
                    if (tilemap.GetTile(cellPosition) == tile && current == 0 && transform.position != waypoints[0])
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
                        game.GetComponent<Game>().IsPatrol = false;
                        game.Patrol();
                    }
                    else
                    {
                        LoadTexture(LastMovementStr);
                    }
                }
            }
            else
            {
                IsPatrol = false;
                if (st == 1)
                {
                    if (transform.position == waypoints[0])
                        current = 0;
                    else if (transform.position == waypoints[1])
                        current = 1;
                    else
                        current = 2;
                    st = 0;
                }
                if (tilemap.GetTile(cellPosition) != tile && this != null)
                {
                    LoadTexture(FirstMovementStr);
                }
                GoToCenter();
            }
        }
    }
    void Patroling()
    {
        if (IsPatrol && (tilemap.GetTile(cellPosition) != tile || current > 1))
        {
            if (transform.position != waypoints[current])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
            else
                current = (current + 1) % waypoints.Length;
        }
        else if(IsPatrol && current!=2)
        {
            current = 0;
            if (transform.position != waypoints[2] && transform.position != waypoints[1])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
        }
    }
    void GoToCenter()
    {
        waypoints = new Vector3[] { gameObject.GetComponent<Character>().startPosition, gameObject.GetComponent<Character>().gatePosition, gameObject.GetComponent<Character>().endPosition, new Vector3(0f,0.5f)};
        if (transform.position != waypoints[3])
        {
            if (tilemap.GetTile(cellPosition) != tile)
            {
                if (transform.position != waypoints[current])
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
                else
                    current = (current + 1) % waypoints.Length;
            }
        }
        else
        {
            LoadTexture();
            Destroy(this);
            game.Patrol();
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
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded && this)
        {
                gameObject.GetComponent<SpriteRenderer>().sprite = handleToCheck.Result;
        }
    }
}
