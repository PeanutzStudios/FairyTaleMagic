using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HairChange : MonoBehaviour
{
    private int CurrentIndex = 0;
    public GameObject ColorHair;
    public GameObject hairoutline;
    public List<Sprite> options = new List<Sprite>(); 
    public List<Sprite> HairOutlineOptions = new List<Sprite>();
    public Image PreviewImage;

    // Separate position offsets for hair and outline
    public List<Vector2> hairPositionOffsets = new List<Vector2>();
    public List<Vector2> outlinePositionOffsets = new List<Vector2>();

    void Start()
    {
        if (ColorHair == null) Debug.LogError("ColorHair is not assigned.");
        if (hairoutline == null) Debug.LogError("Hair outline is not assigned.");
        if (PreviewImage == null) Debug.LogError("PreviewImage is not assigned.");
        if (options.Count == 0) Debug.LogWarning("No sprites in options list. Please assign sprites in the Inspector.");
        
        // Load sprite index and position on start
        LoadSprite();
        UpdatePosition(); // Update position on start
    }

    // Save the selected sprite index to PlayerPrefs
    private void SaveSprite()
    {
        if (options.Count > 0 && CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            PlayerPrefs.SetInt("selectedSpriteIndex", CurrentIndex);
            PlayerPrefs.Save();
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
        CurrentIndex = PlayerPrefs.GetInt("selectedSpriteIndex", 0);
        if (CurrentIndex >= 0 && CurrentIndex < options.Count)
        {
            Debug.Log("Loaded sprite index: " + CurrentIndex);
        }
        else
        {
            Debug.LogWarning("No saved sprite index or invalid index. Defaulting to 0.");
            CurrentIndex = 0;
        }
        UpdatePreview();
        UpdatePrefab();
    }

    // Update the prefab's sprite and position based on the saved index
    private void UpdatePrefab()
    {
        // Update the outline sprite and position
        if (hairoutline != null && HairOutlineOptions.Count > 0 && CurrentIndex < HairOutlineOptions.Count)
        {
            GameObject outlineInstance = GameObject.Find(hairoutline.name);
            if (outlineInstance != null)
            {
                SpriteRenderer outlineRenderer = outlineInstance.GetComponent<SpriteRenderer>();
                if (outlineRenderer != null)
                {
                    outlineRenderer.sprite = HairOutlineOptions[CurrentIndex];
                    Debug.Log("Updated outline sprite: " + HairOutlineOptions[CurrentIndex].name);
                }
                else
                {
                    Debug.LogError("Outline prefab does not have a SpriteRenderer component.");
                }

                // Adjust outline position using its own offset
                if (outlinePositionOffsets.Count > CurrentIndex)
                {
                    Vector2 outlinePositionOffset = outlinePositionOffsets[CurrentIndex];
                    outlineInstance.transform.localPosition = outlinePositionOffset;
                    Debug.Log("Updated outline position: " + outlinePositionOffset);
                }
                else
                {
                    Debug.LogWarning("No outline position offset found for this index.");
                }
            }
            else
            {
                Debug.LogError("Outline prefab instance not found in the scene!");
            }
        }

        // Update the hair sprite and position
        if (ColorHair != null && options.Count > 0 && CurrentIndex < options.Count)
        {
            GameObject hairInstance = GameObject.Find(ColorHair.name);
            if (hairInstance != null)
            {
                SpriteRenderer hairRenderer = hairInstance.GetComponent<SpriteRenderer>();
                if (hairRenderer != null)
                {
                    hairRenderer.sprite = options[CurrentIndex];
                    Debug.Log("Updated hair sprite: " + options[CurrentIndex].name);
                }
                else
                {
                    Debug.LogError("Hair prefab does not have a SpriteRenderer component.");
                }

                // Adjust hair position using its own offset
                if (hairPositionOffsets.Count > CurrentIndex)
                {
                    Vector2 positionOffset = hairPositionOffsets[CurrentIndex];
                    hairInstance.transform.localPosition = positionOffset;
                    Debug.Log("Updated hair position: " + positionOffset);
                }
                else
                {
                    Debug.LogWarning("No hair position offset found for this index.");
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
        }
        else
        {
            Debug.LogWarning("PreviewImage is null or current index is out of range.");
        }
    }

    // Update the position of both hair and outline
    private void UpdatePosition()
    {
        if (hairPositionOffsets.Count > CurrentIndex)
        {
            Vector2 hairPositionOffset = hairPositionOffsets[CurrentIndex];
            if (ColorHair != null)
            {
                ColorHair.transform.localPosition = hairPositionOffset;
                Debug.Log("Updated hair position on start: " + hairPositionOffset);
            }
        }

        if (outlinePositionOffsets.Count > CurrentIndex)
        {
            Vector2 outlinePositionOffset = outlinePositionOffsets[CurrentIndex];
            if (hairoutline != null)
            {
                hairoutline.transform.localPosition = outlinePositionOffset;
                Debug.Log("Updated outline position on start: " + outlinePositionOffset);
            }
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
            SaveSprite();
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
            SaveSprite();
        }
        else
        {
            Debug.LogError("Options list is empty.");
        }
    }
}
