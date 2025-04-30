using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    private int CurrentIndex = 0;
    public GameObject Prefab; // Reference to the prefab in the scene
    public List<GameObject> options = new List<GameObject>(); // List of available GameObjects
    public Image PreviewImage; // UI element to preview the selected GameObject

    void Start()
    {
        if (Prefab == null) Debug.LogError("Prefab is not assigned.");
        if (PreviewImage == null) Debug.LogError("PreviewImage is not assigned.");
        if (options.Count == 0) Debug.LogWarning("No GameObjects in options list. Please assign GameObjects in the Inspector.");
        
        // Load GameObject index on start
        LoadGameObject();
    }

    // Save the selected GameObject index to PlayerPrefs
    private void SaveGameObject()
    {
        if (options.Count > 0 && CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            PlayerPrefs.SetInt("selectedGameObjectIndex", CurrentIndex);
            PlayerPrefs.Save(); // Ensures immediate save to PlayerPrefs
            Debug.Log("Saved GameObject index: " + CurrentIndex);
        }
        else
        {
            Debug.LogError("Options list is empty or index out of bounds.");
        }
    }

    // Load the saved GameObject index from PlayerPrefs
    private void LoadGameObject()
    {
        CurrentIndex = PlayerPrefs.GetInt("selectedGameObjectIndex", 0); // Default to 0 if nothing is saved
        if (CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            Debug.Log("Loaded GameObject index: " + CurrentIndex);
        }
        else
        {
            Debug.LogWarning("No saved GameObject index or invalid index. Defaulting to 0.");
            CurrentIndex = 0; // Default to the first GameObject if nothing is saved
        }
        UpdatePreview();
        UpdatePrefab();
    }

    // Update the prefab based on the saved index
    private void UpdatePrefab()
    {
        if (Prefab != null && options.Count > 0 && CurrentIndex < options.Count)
        {
            GameObject prefabInstance = GameObject.Find(Prefab.name);
            if (prefabInstance != null)
            {
                // Assuming the prefab has a SpriteRenderer component
                SpriteRenderer spriteRenderer = prefabInstance.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Assuming the GameObject in the options list has a SpriteRenderer component
                    SpriteRenderer optionSpriteRenderer = options[CurrentIndex].GetComponent<SpriteRenderer>();
                    if (optionSpriteRenderer != null)
                    {
                        spriteRenderer.sprite = optionSpriteRenderer.sprite;
                        Debug.Log("Updated sprite in prefab: " + optionSpriteRenderer.sprite.name);
                    }
                    else
                    {
                        Debug.LogError("GameObject in options list does not have a SpriteRenderer component.");
                    }
                }
                else
                {
                    Debug.LogError("Prefab does not have a SpriteRenderer component.");
                }
            }
            else
            {
                Debug.LogError("Prefab instance not found in the scene!");
            }
        }
        else
        {
            Debug.LogWarning("Prefab is null or current index is out of range.");
        }
    }

    // Update the preview image based on the selected GameObject index
    private void UpdatePreview()
    {
        if (PreviewImage != null && options.Count > 0 && CurrentIndex < options.Count)
        {
            // Assuming the GameObject in the options list has a SpriteRenderer component
            SpriteRenderer optionSpriteRenderer = options[CurrentIndex].GetComponent<SpriteRenderer>();
            if (optionSpriteRenderer != null)
            {
                PreviewImage.sprite = optionSpriteRenderer.sprite;
                Debug.Log("Updated preview image with sprite: " + optionSpriteRenderer.sprite.name);
            }
            else
            {
                Debug.LogError("GameObject in options list does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogWarning("PreviewImage is null or current index is out of range.");
        }
    }

    // Go to the next GameObject in the options list
    public void Next()
    {
        if (options.Count > 0)
        {
            CurrentIndex = (CurrentIndex + 1) % options.Count;
            UpdatePrefab();
            UpdatePreview();
            SaveGameObject(); // Save the selected GameObject
        }
        else
        {
            Debug.LogError("Options list is empty.");
        }
    }

    // Go to the previous GameObject in the options list
    public void Prev()
    {
        if (options.Count > 0)
        {
            CurrentIndex = (CurrentIndex - 1 + options.Count) % options.Count;
            UpdatePrefab();
            UpdatePreview();
            SaveGameObject(); // Save the selected GameObject
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
            SaveGameObject();
            Debug.Log("Application lost focus, saved GameObject index.");
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveGameObject();
            Debug.Log("Application paused, saved GameObject index.");
        }
    }

    void OnApplicationQuit()
    {
        SaveGameObject();
        Debug.Log("Application is quitting, saved GameObject index.");
    }
}