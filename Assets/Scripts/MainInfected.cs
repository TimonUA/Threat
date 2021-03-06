﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class MainInfected : MonoBehaviour
{
    private Vector3[] waypoints;
    private Vector3Int cellPosition;
    private Vector2 GateCoordinates;
    private bool IsPatrol;
    private int current;
    public float speed;
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
        waypoints = new Vector3[] { gameObject.GetComponent<Character>().startPosition, gameObject.GetComponent<Character>().gatePosition, gameObject.GetComponent<Character>().endPosition, new Vector3(0f, 0.5f) };
        if (transform.position == waypoints[0])
            current = 0;
        else if (transform.position == waypoints[1])
            current = 1;
        else
            current = 2;
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
        if (!PauseMenu.IsPaused && !Game.IsEnd && !DialogueManager.IsDialogue)
        {
            
                Patroling();
                if (downTime > 0)
                {
                    if (transform.position == waypoints[3])
                    {
                        IsPatrol = false;
                        LoadTexture();
                        game.Patrol();
                        Destroy(this);
                    }
                    if(current==3)
                    {
                    if (gameObject.GetComponent<SpriteRenderer>().sprite != gameObject.GetComponent<Character>().mainSprite)
                        LoadTexture();      
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
                    LoadTexture();
                    gameObject.tag = "Character";
                    Destroy(this);
                    game.RePosition();
                        
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
        else if (IsPatrol && current < 2)
        {
            current = 0;
            if (transform.position != waypoints[2] && transform.position != waypoints[1])
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);
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

