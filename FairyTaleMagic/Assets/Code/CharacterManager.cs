using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ShirtStorage : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject hairOutline;
    public List<Sprite> options = new List<Sprite>();
    public List<Sprite> HairOutlineOptions = new List<Sprite>();

    private int CurrentIndex = 0;

    void OnEnable()
    {
        // Hook into scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Apply saved data after scene load
        LoadSprite();
        LoadPosition();
        LoadOutlinePosition();
    }

    void Start()
    {
        // Initial load when the scene is first loaded
        LoadSprite();
        LoadPosition();
        LoadOutlinePosition();
    }

    private void LoadSprite()
    {
        CurrentIndex = PlayerPrefs.GetInt("ShirtIndex", 0);
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
}
