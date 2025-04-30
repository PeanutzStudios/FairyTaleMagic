using UnityEngine;
using System.Collections.Generic;

public class PrefabSpriteLoader : MonoBehaviour
{
    public GameObject Prefab;              // Reference to the main prefab
    public GameObject hairOutline;         // Reference to the hair outline prefab
    public List<Sprite> options = new List<Sprite>();    // List of available sprites for the prefab
    public List<Sprite> HairOutlineOptions = new List<Sprite>();  // List of available sprites for the hair outline

    void Start()
    {
        // Load sprite index on scene start
        LoadSprite();
    }

    // Load the saved sprite index from PlayerPrefs and apply to prefab
    private void LoadSprite()
    {
        int index = PlayerPrefs.GetInt("selectedSpriteIndex", 0); // Default to 0 if nothing is saved
        if (index >= 0 && index < options.Count)
        {
            ApplySpriteToPrefab(index);
        }
        else
        {
            Debug.LogWarning("No saved sprite index or invalid index. Defaulting to 0.");
            ApplySpriteToPrefab(0); // Default to the first sprite
        }
    }

    // Apply the loaded sprite to both the hair outline and the main prefab's sprite renderer
    private void ApplySpriteToPrefab(int index)
    {
        // Apply sprite to hair outline
        if (hairOutline != null && HairOutlineOptions.Count > 0 && index < HairOutlineOptions.Count)
        {
            ApplySpriteToRenderer(hairOutline, HairOutlineOptions[index]);
        }

        // Apply sprite to the main prefab
        if (Prefab != null && options.Count > 0 && index < options.Count)
        {
            ApplySpriteToRenderer(Prefab, options[index]);
        }
    }

    // Helper method to apply a sprite to a given GameObject's SpriteRenderer
    private void ApplySpriteToRenderer(GameObject targetObject, Sprite sprite)
    {
        if (targetObject != null)
        {
            SpriteRenderer spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = sprite;
                Debug.Log("Applied sprite: " + sprite.name);
            }
            else
            {
                Debug.LogError("Target object does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogWarning("Target object is null.");
        }
    }
}
