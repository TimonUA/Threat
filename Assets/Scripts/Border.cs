using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Border : MonoBehaviour
{
    private string texture;
    private string borderStr = "Border_";
    // Start is called before the first frame update
    void Start()
    {
        texture = gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite.name;
        if (texture.Contains("astronautA"))
            texture = texture.Replace("astronautA_", borderStr);
        else if(texture.Contains("astronautB"))
            texture = texture.Replace("astronautB_", borderStr);
        else
            texture = texture.Replace("alien_", borderStr);
        LoadTexture(texture);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        texture = gameObject.transform.parent.GetComponent<SpriteRenderer>().sprite.name;
        if (texture.Contains("astronautA"))
            texture = texture.Replace("astronautA_", borderStr);
        else if (texture.Contains("astronautB"))
            texture = texture.Replace("astronautB_", borderStr);
        else
            texture = texture.Replace("alien_", borderStr);
        LoadTexture(texture);
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
