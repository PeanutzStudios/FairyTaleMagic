using UnityEngine;
using UnityEngine.SceneManagement;

public class DontProsist : MonoBehaviour
{
    public string Scenename; // Replace with your main menu scene name

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Scenename)
        {
            // Destroy the persistent asset when returning to the main menu
            Destroy(this.gameObject);
        }
    }

    

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}