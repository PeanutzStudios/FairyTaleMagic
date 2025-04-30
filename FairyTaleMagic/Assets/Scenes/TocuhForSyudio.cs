using UnityEngine;
using UnityEngine.SceneManagement;
using TS.ColorPicker;

public class MovePrefabWithTouch : MonoBehaviour
{
    public float moveSpeed = 5f;
    public string targetSceneName = "Studio"; // Name of the scene where this script should work
    private Vector3 initialPosition; // Store the initial position of the object

    void Awake()
    {
        // Store the initial position of the object when the game starts
        initialPosition = transform.position;

        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Prevent movement if the color picker is open
        if (ColorPickerForBackground.IsColorPickerOpen) 
            return;

        // Check if the current scene is the target scene
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            // Handle touch input (on devices)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check if the touch is moving (dragging)
                if (touch.phase == TouchPhase.Moved)
                {
                    // Convert touch position to world position
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                    // Move the GameObject in all directions based on touch input
                    Vector3 newPosition = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
                }
            }
            // Handle mouse input (in the Editor)
            else if (Input.GetMouseButton(0)) // Left mouse button held down
            {
                // Convert mouse position to world position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

                // Move the GameObject in all directions based on mouse input
                Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
            }
        }
    }

    // Called when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the Main Menu
        if (scene.name == "GameMenu") // Replace with your main menu's name
        {
            // Reset the object's position to the initial position
            transform.position = initialPosition;
        }
    }

    // Unsubscribe from the scene loaded event when the object is destroyed
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
