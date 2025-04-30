using UnityEngine;
using UnityEngine.UI;

public class hideUi : MonoBehaviour
{
    public GameObject pausemenu;
    public Button Button;
    public bool IsHidden;

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(true);  // Initially show the pause menu
        IsHidden = false;  // Start with the pause menu visible
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input (mouse or touch) to unhide the UI
        Unhide();
    }

    // Function to toggle visibility when the button is clicked
    public void HiddenButton()
    {
        if (IsHidden)
        {
            nothide(); // If it's hidden, show it
        }
        else
        {
            Hidden(); // If it's visible, hide it
        }
    }

    // Function to unhide (show) the menu
    public void Unhide()
    {
        // Check if there was a mouse click or a touch on the screen
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (IsHidden)  // Only unhide if it's currently hidden
            {
                nothide();  // Show the menu
            }
        }
    }

    // Function to hide the menu
    public void Hidden()
    {
        pausemenu.SetActive(false); // Hide the menu
        IsHidden = true;  // Update the state to hidden
    }

    // Function to show the menu
    public void nothide()
    {
        pausemenu.SetActive(true); // Show the menu
        IsHidden = false;  // Update the state to not hidden
    }
}
