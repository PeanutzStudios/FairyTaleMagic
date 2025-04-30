using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomCode : MonoBehaviour
{
    public static CustomCode instance;

    // Scene names to manage object persistence
    private string trophySceneName = "Trophy";
    private string mainMenuSceneName = "Menu"; // Example: Main Menu where the object should be reloaded
private string HairScene = "Haircustomize"; 

private string ClothingScene = "Clothing"; 
private string GameNenu = "GameMenu"; 
    void Awake()
    {
        Debug.Log("Awake called on: " + gameObject.name);

        // If no instance exists, assign this one as the persistent instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep this object alive across scene loads
            Debug.Log("First instance created: " + gameObject.name);
        }
        else if (instance != this)
        {
            // If an instance already exists, rename the duplicate object to avoid confusion
            gameObject.name = gameObject.name + "_Duplicate_" + System.Guid.NewGuid().ToString();
            Debug.Log("Duplicate object found. Renaming: " + gameObject.name);
            Destroy(gameObject);  // Destroy the duplicate object
        }
    }

    // Handle scene changes
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        // Check if we are entering the Trophy scene
        if (scene.name == trophySceneName || scene.name == mainMenuSceneName || scene.name == HairScene|| scene.name == ClothingScene)
        {
            // Destroy the persistent object when entering the Trophy scene
            Destroy(gameObject);
            Debug.Log(gameObject.name + " destroyed because we are entering the Trophy scene.");
        }

        // Reload the object when returning to the Main Menu or other specific scenes
        if (scene.name == GameNenu)
        {
            if (gameObject.name.Contains("_Duplicate_"))
            {
                // If it's a duplicate object, re-enable it or recreate the object as needed.
                Debug.Log("Re-enabling or reloading the object back in the scene.");
                // You can set the object's active state here if needed
                gameObject.SetActive(true);  // Example of setting it back active if required
            }
        }
    }

    // Subscribe to scene loading event
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe when the object is disabled or destroyed
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
