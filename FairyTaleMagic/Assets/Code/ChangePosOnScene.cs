using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScenePositionManager : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 originalScale;  // Store the original scale

    // Dictionary to store positions for each scene
    private Dictionary<string, Vector3> scenePositions = new Dictionary<string, Vector3>()
    {
        { "GameMenu", new Vector3(-5f, -0.2959347f, 0) },
        { "Posses", new Vector3(-6.1f, -1.1f, 0) },
        { "Haircustomize", new Vector3(-6.72f, -2.06f, 0) },
        { "Scene3", new Vector3(1, -1, 0) }
    };

    // Dictionary to store scales for each scene
    private Dictionary<string, Vector3> sceneScales = new Dictionary<string, Vector3>()
    {
        { "GameMenu", new Vector3(1, 1, 1) },
        { "Posses", new Vector3(1f, 1f, 1) },
        { "Haircustomize", new Vector3(1.221f, 1.221f, 1) },
        { "Scene3", new Vector3(0.8f, 0.8f, 1) }
    };

    private void Awake()
    {
        // Store the original position and scale
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    private void Start()
    {
        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Change the position when entering a new scene
        if (scenePositions.ContainsKey(scene.name))
        {
            transform.position = scenePositions[scene.name];
        }
        else
        {
            transform.position = originalPosition;  // Reset position if scene is not in the dictionary
        }

        // Change the scale when entering a new scene
        if (sceneScales.ContainsKey(scene.name))
        {
            transform.localScale = sceneScales[scene.name];
        }
        else
        {
            transform.localScale = originalScale;  // Reset scale if scene is not in the dictionary
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
