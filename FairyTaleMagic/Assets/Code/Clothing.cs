using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Clothing : MonoBehaviour
{
    // General Variables for Customization
    public GameObject character; // Reference to the character GameObject
    public Image previewImage; // UI Image for the preview
    public List<Sprite> options = new List<Sprite>(); // Available sprites

    private int currentOption; // Current selected option index

    // Unique Keys for Each Prefab's Customization
    private string optionKey; // Key for storing the option index in PlayerPrefs
    private string characterSpriteKey; // Key for storing the sprite name in PlayerPrefs

    void Start()
    {
        LoadCurrentOption(); // Load the saved option index for this prefab
        LoadCharacterState(); // Load the saved character sprite state
        UpdatePreview(); // Refresh the preview
    }

    // Initialize method to allow each prefab to have its own unique keys
    public void Initialize(string optionKey, string characterSpriteKey)
    {
        this.optionKey = optionKey;
        this.characterSpriteKey = characterSpriteKey;
    }

    // Button methods for changing the customization
    public void Next()
    {
        currentOption = (currentOption + 1) % options.Count; // Increment and loop back
        UpdateSprites();
    }

    public void Prev()
    {
        currentOption = (currentOption - 1 + options.Count) % options.Count; // Decrement and loop back
        UpdateSprites();
    }

    // Updates the sprite and preview
    private void UpdateSprites()
    {
        if (options.Count > 0 && currentOption < options.Count)
        {
            // Set the character's SpriteRenderer to the selected option
            character.GetComponent<SpriteRenderer>().sprite = options[currentOption]; // Set current sprite
            UpdatePreview(); // Refresh the preview sprite
            SaveCurrentOption(); // Save current selection to PlayerPrefs
            SaveCharacterState(); // Save the entire character state
        }
        else
        {
            Debug.LogWarning($"Selected option {currentOption} is out of range.");
        }
    }

    // Update the preview image in UI
    private void UpdatePreview()
    {
        if (previewImage != null && currentOption < options.Count)
        {
            previewImage.sprite = options[currentOption]; // Update the preview image
            Debug.Log($"Preview image updated to: {options[currentOption].name} (Index: {currentOption})");
        }
        else
        {
            Debug.LogWarning("Preview image is null or current option is out of range.");
            if (previewImage != null) previewImage.sprite = null; // Clear if current option is invalid
        }
    }

    // Save the current option to PlayerPrefs
    private void SaveCurrentOption()
    {
        PlayerPrefs.SetInt(optionKey, currentOption); // Save selection to PlayerPrefs
        PlayerPrefs.Save(); // Persist the changes
        Debug.Log($"Saved Current Option: {currentOption}");
    }

    // Load the current option from PlayerPrefs
    private void LoadCurrentOption()
    {
        // Load the saved option if one exists for the specific prefab
        if (PlayerPrefs.HasKey(optionKey))
        {
            currentOption = PlayerPrefs.GetInt(optionKey); // Load saved option
            Debug.Log($"Loaded Current Option: {currentOption}");

            // Ensure the loaded value does not exceed the range of available options
            if (currentOption < 0 || currentOption >= options.Count)
            {
                currentOption = 0; // Default to 0 if out of range
                Debug.Log($"Current option is out of range, defaulting to option 0.");
            }
        }
        else
        {
            currentOption = 0; // Default to the first option if nothing was saved
            Debug.Log("No previous option found; defaulting to option 0.");
        }
    }

    // Save the character state (sprite name)
    private void SaveCharacterState()
    {
        if (character.GetComponent<SpriteRenderer>().sprite != null)
        {
            string spriteName = character.GetComponent<SpriteRenderer>().sprite.name; // Get sprite name
            PlayerPrefs.SetString(characterSpriteKey, spriteName); // Save sprite name
            PlayerPrefs.Save(); // Ensure data is saved
            Debug.Log($"Saved Character Sprite: {spriteName}");
        }
    }

    // Load the character state from PlayerPrefs
    private void LoadCharacterState()
    {
        // Load the character state for this specific prefab
        if (PlayerPrefs.HasKey(characterSpriteKey))
        {
            string spriteName = PlayerPrefs.GetString(characterSpriteKey);
            Debug.Log($"Loading Character Sprite: {spriteName}");

            bool spriteFound = false;

            foreach (var option in options)
            {
                if (option.name == spriteName)
                {
                    currentOption = options.IndexOf(option);
                    character.GetComponent<SpriteRenderer>().sprite = option; // Set the character's sprite
                    Debug.Log($"Character Sprite loaded: {spriteName} at index {currentOption}");
                    UpdatePreview(); // Update the preview image
                    spriteFound = true;
                    break; // Exit after finding the match
                }
            }

            if (!spriteFound)
            {
                Debug.LogWarning("Loaded sprite name does not match any option; using default sprite.");
                character.GetComponent<SpriteRenderer>().sprite = options[0]; // Default to the first sprite
            }
        }
        else
        {
            Debug.Log("No sprite name found; using default sprite.");
            character.GetComponent<SpriteRenderer>().sprite = options[0]; // Ensure a default sprite is set
        }
    }
}
