using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject persistentObject;  // Reference to the persistent object

    private void Start()
    {
        // Register to listen for scene load events.
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Ensure the GameObject is persistent across scenes, only if it's the first scene
        if (persistentObject != null)
        {
            DontDestroyOnLoad(persistentObject);
        }
    }

    // When a new scene is loaded, check which scene it is
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the current scene is the one to exclude (e.g., "MainMenu"), destroy the object
        if (scene.name == "Menu")  // Replace with the name of your excluded scene
        {
            if (persistentObject != null)
            {
                Debug.Log("Destroying persistent object in MainMenu scene");
                Destroy(persistentObject);
            }
        }
    }

    // Optional cleanup when the GameManager is destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe to prevent memory leaks
    }
}
