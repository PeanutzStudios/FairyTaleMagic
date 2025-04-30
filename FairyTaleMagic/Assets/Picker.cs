using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
public class CustomizationScreenPrefabSwitcher : MonoBehaviour
{
    private int CurrentIndex = 0;
    public GameObject ColorHair;

    public List<Vector2> hairColorResize = new List<Vector2>();
     public List<Vector2> hairResize = new List<Vector2>();
    
    public GameObject hairoutline; // Reference to the prefab in the scene
    public List<Sprite> options = new List<Sprite>(); 
    
     public List<Sprite> HairOutlineOptions = new List<Sprite>();// List of available sprites
    public Image PreviewImage; // UI element to preview the selected sprite

    void Start()
    {
        if (ColorHair == null) Debug.LogError("Prefab is not assigned.");
        if (PreviewImage == null) Debug.LogError("PreviewImage is not assigned.");
        if (options.Count == 0) Debug.LogWarning("No sprites in options list. Please assign sprites in the Inspector.");
        
        // Load sprite index on start
        LoadSprite();
    }

    // Save the selected sprite index to PlayerPrefs
    private void SaveSprite()
    {
        if (options.Count > 0 && CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            PlayerPrefs.SetInt("selectedSpriteIndex", CurrentIndex);
            PlayerPrefs.Save(); // Ensures immediate save to PlayerPrefs
            Debug.Log("Saved sprite index: " + CurrentIndex);
        }
        else
        {
            Debug.LogError("Options list is empty or index out of bounds.");
        }
    }

    // Load the saved sprite index from PlayerPrefs
    private void LoadSprite()
    {
        CurrentIndex = PlayerPrefs.GetInt("selectedSpriteIndex", 0); // Default to 0 if nothing is saved
        if (CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            Debug.Log("Loaded sprite index: " + CurrentIndex);
        }
        else
        {
            Debug.LogWarning("No saved sprite index or invalid index. Defaulting to 0.");
            CurrentIndex = 0; // Default to the first sprite if nothing is saved
        }
        UpdatePreview();
        UpdatePrefab();
    }

    // Update the prefab's sprite based on the saved index
private void UpdatePrefab()
{
    // Update the outline sprite if it exists and has a valid index
    if (hairoutline != null && HairOutlineOptions.Count > 0 && CurrentIndex < HairOutlineOptions.Count)
    {
        GameObject outlineInstance = GameObject.Find(hairoutline.name);
        if (outlineInstance != null)
        {
            SpriteRenderer outlineRenderer = outlineInstance.GetComponent<SpriteRenderer>();
            if (outlineRenderer != null)
            {
                outlineRenderer.sprite = HairOutlineOptions[CurrentIndex];  // Update the outline sprite
                Debug.Log("Updated outline sprite: " + HairOutlineOptions[CurrentIndex].name);
            }
            else
            {
                Debug.LogError("Outline prefab does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Outline prefab instance not found in the scene!");
        }
    }

    // Update the hair sprite if it exists and has a valid index
    if (ColorHair != null && options.Count > 0 && CurrentIndex < options.Count)
    {
        GameObject hairInstance = GameObject.Find(ColorHair.name);
        if (hairInstance != null)
        {
            SpriteRenderer hairRenderer = hairInstance.GetComponent<SpriteRenderer>();
            if (hairRenderer != null)
            {
                hairRenderer.sprite = options[CurrentIndex];  // Update the hair sprite
                Debug.Log("Updated hair sprite: " + options[CurrentIndex].name);
            }
            else
            {
                Debug.LogError("Hair prefab does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Hair prefab instance not found in the scene!");
        }
    }
    else
    {
        Debug.LogWarning("Hair prefab is null or current index is out of range.");
    }
}


    // Update the preview image based on the selected sprite index
    private void UpdatePreview()
    {
        if (PreviewImage != null && options.Count > 0 && CurrentIndex < options.Count)
        {
            PreviewImage.sprite = options[CurrentIndex];
            
            Debug.Log("Updated preview image with sprite: " + options[CurrentIndex].name);
            Debug.Log("Updated preview image with sprite: " + HairOutlineOptions[CurrentIndex].name);
        }
        else
        {
            Debug.LogWarning("PreviewImage is null or current index is out of range.");
        }
    }

    // Go to the next sprite in the options list
    public void Next()
    {
        if (options.Count > 0)
        {
            CurrentIndex = (CurrentIndex + 1) % options.Count;
            UpdatePrefab();
            UpdatePreview();
            SaveSprite(); // Save the selected sprite
        }
        else
        {
            Debug.LogError("Options list is empty.");
        }
    }

    // Go to the previous sprite in the options list
    public void Prev()
    {
        if (options.Count > 0)
        {
            CurrentIndex = (CurrentIndex - 1 + options.Count) % options.Count;
            UpdatePrefab();
            UpdatePreview();
            SaveSprite(); // Save the selected sprite
        }
        else
        {
            Debug.LogError("Options list is empty.");
        }
    }

    // Automatically save when the application loses focus or pauses
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveSprite();
            Debug.Log("Application lost focus, saved sprite index.");
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveSprite();
            Debug.Log("Application paused, saved sprite index.");
        }
    }

    void OnApplicationQuit()
    {
        SaveSprite();
        Debug.Log("Application is quitting, saved sprite index.");
    }
}
