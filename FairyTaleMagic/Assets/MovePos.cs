using UnityEngine;
using UnityEngine.EventSystems;

public class PrefabMover : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab you want to move
    public RectTransform panel; // The panel that defines the touch area
    private Vector3 dragOffset;
    private bool isDragging = false;

    private string prefabPositionKey = "PrefabPosition"; // PlayerPrefs key for saving position

    void Start()
    {
        LoadPosition(); // Load the saved position when the scene starts
    }

    void Update()
    {
        // Debugging: Check if the pointer is over the panel
        if (IsPointerOverPanel())
        {
            Debug.Log("Pointer is inside the panel area!");

            if (Input.GetMouseButtonDown(0)) // Mouse down (or touch start)
            {
                Debug.Log("Drag started!");

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragOffset = prefab.transform.position - mousePos;
                isDragging = true;
            }

            if (isDragging && Input.GetMouseButton(0)) // While dragging
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + dragOffset;

                // Clamp the movement within the panel bounds
                Vector3 clampedPosition = new Vector3(
                    Mathf.Clamp(mousePos.x, panel.rect.xMin, panel.rect.xMax),
                    Mathf.Clamp(mousePos.y, panel.rect.yMin, panel.rect.yMax),
                    prefab.transform.position.z
                );

                prefab.transform.position = clampedPosition;
                Debug.Log("Prefab moved to: " + prefab.transform.position);
            }

            if (Input.GetMouseButtonUp(0)) // Mouse up (or touch end)
            {
                Debug.Log("Drag ended!");
                isDragging = false;
                SavePosition(); // Save position when the drag ends
            }
        }
        else
        {
            Debug.Log("Pointer is outside the panel.");
        }
    }

    // Check if the pointer (mouse or touch) is over the panel's rect area
    bool IsPointerOverPanel()
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, Camera.main, out localPos);
        return panel.rect.Contains(localPos);
    }

    // Save the prefab's position to PlayerPrefs
    void SavePosition()
    {
        PlayerPrefs.SetFloat(prefabPositionKey + "_X", prefab.transform.position.x);
        PlayerPrefs.SetFloat(prefabPositionKey + "_Y", prefab.transform.position.y);
        PlayerPrefs.SetFloat(prefabPositionKey + "_Z", prefab.transform.position.z);
        PlayerPrefs.Save(); // Make sure changes are saved
        Debug.Log("Position saved!");
    }

    // Load the prefab's position from PlayerPrefs
    void LoadPosition()
    {
        if (PlayerPrefs.HasKey(prefabPositionKey + "_X"))
        {
            float x = PlayerPrefs.GetFloat(prefabPositionKey + "_X");
            float y = PlayerPrefs.GetFloat(prefabPositionKey + "_Y");
            float z = PlayerPrefs.GetFloat(prefabPositionKey + "_Z");
            prefab.transform.position = new Vector3(x, y, z);
            Debug.Log("Position loaded: " + prefab.transform.position);
        }
    }
}
