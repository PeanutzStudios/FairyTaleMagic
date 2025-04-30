using UnityEngine;
using System.Collections.Generic;

public class FrontHair : MonoBehaviour
{
    public GameObject Prefab;                   // Reference to the main prefab
    public GameObject hairOutline;               // Reference to the hair outline prefab
    public List<Sprite> options = new List<Sprite>();        // List of available sprites for the prefab
    public List<Sprite> HairOutlineOptions = new List<Sprite>(); // List of available sprites for the hair outline
    
    private int CurrentIndex = 0;

    void Start()
    {
        // Load sprite index and position on scene start
        LoadSprite();
        LoadPosition();
        LoadOutlinePosition(); // Load outline position on start
    }

    // Save the position of the prefab
    private void SavePosition()
    {
        if (Prefab != null)
        {
            PlayerPrefs.SetFloat("PrefabPosX", Prefab.transform.localPosition.x);
            PlayerPrefs.SetFloat("PrefabPosY", Prefab.transform.localPosition.y);
            PlayerPrefs.Save(); // Ensure data is written immediately
            Debug.Log("Saved Prefab position: " + Prefab.transform.localPosition);
        }
    }

    // Load the saved position of the prefab
    private void LoadPosition()
    {
        if (Prefab != null)
        {
            float x = PlayerPrefs.GetFloat("PrefabPosX", Prefab.transform.localPosition.x);
            float y = PlayerPrefs.GetFloat("PrefabPosY", Prefab.transform.localPosition.y);
            Prefab.transform.localPosition = new Vector2(x, y);
            Debug.Log("Loaded Prefab position: " + Prefab.transform.localPosition);
        }
    }

    // Save the position of the hair outline
    private void SaveOutlinePosition()
    {
        if (hairOutline != null)
        {
            PlayerPrefs.SetFloat("OutlinePosX", hairOutline.transform.localPosition.x);
            PlayerPrefs.SetFloat("OutlinePosY", hairOutline.transform.localPosition.y);
            PlayerPrefs.Save(); // Ensure data is written immediately
            Debug.Log("Saved Outline position: " + hairOutline.transform.localPosition);
        }
    }

    // Load the saved position of the hair outline
    private void LoadOutlinePosition()
    {
        if (hairOutline != null)
        {
            float x = PlayerPrefs.GetFloat("OutlinePosX", hairOutline.transform.localPosition.x);
            float y = PlayerPrefs.GetFloat("OutlinePosY", hairOutline.transform.localPosition.y);
            hairOutline.transform.localPosition = new Vector2(x, y);
            Debug.Log("Loaded Outline position: " + hairOutline.transform.localPosition);
        }
    }

    // Save the selected sprite index
    private void SaveSpriteIndex()
    {
        PlayerPrefs.SetInt("selectedSpriteIndex", CurrentIndex);
        PlayerPrefs.Save(); // Ensure data is written immediately
        Debug.Log("Saved sprite index: " + CurrentIndex);
    }

    // Load the saved sprite index from PlayerPrefs and apply to prefab
    private void LoadSprite()
    {
        CurrentIndex = PlayerPrefs.GetInt("selectedSpriteIndex", 0);
        if (CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            ApplySpriteToPrefab(CurrentIndex);
        }
        else
        {
            Debug.LogWarning("No saved sprite index or invalid index. Defaulting to 0.");
            ApplySpriteToPrefab(0);
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

    // Save positions when the application loses focus or pauses
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SavePosition();
            SaveOutlinePosition(); // Save outline position
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SavePosition();
            SaveOutlinePosition(); // Save outline position
        }
    }

    void OnApplicationQuit()
    {
        SavePosition();
        SaveOutlinePosition(); // Save outline position
    }
}
