using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance { get; private set; }

    // Store the active panel state
    public string ActivePanelState { get; set; } = "MainPanel"; // Default state

    // Store the highlighted button state
    public string HighlightedButtonState { get; set; } = "MenuButton"; // Default state

    private void Awake()
    {
        // Ensure only one instance of PersistentDataManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}