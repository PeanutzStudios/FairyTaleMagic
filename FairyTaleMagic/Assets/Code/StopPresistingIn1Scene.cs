using UnityEngine;
using UnityEngine.SceneManagement;

public class StopProsistingInScene1 : MonoBehaviour
{
    public GameObject objectToReset;  // Drag the object to reset in the Inspector
    public Vector3 resetPosition;
    private string mainMenuSceneName = "Menu"; // Replace with your main menu scene name
    public string sceneToResetOn;
 
    private Vector3 initialPosition;
    public static StopProsistingInScene1 instance;
    private string TrophyRoom = "Trophy";

    void Awake()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the scene loaded and perform actions accordingly
        if (scene.name == TrophyRoom)
        {
            // Disable the objects when the scene is loaded
            if (gameObject.name == "PleasWork" || gameObject.name == "Belt 1_0")
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + " disabled in scene: " + scene.name);
            }
        }

        if (scene.name == mainMenuSceneName)
        {
            // Disable the object when the menu scene is loaded
            if (gameObject.name == "PleasWork" || gameObject.name == "Belt 1_0")
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + " disabled in menu scene");
            }
        }
    }

    // Cleanup when the object is destroyed or the game is stopped
    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
