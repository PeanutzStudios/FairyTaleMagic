using UnityEngine;

public class HideMainUI : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public bool IsHidden;
    private GameObject playerObject;
    private Vector3 originalPlayerPosition;

    void Start()
    {
        // Find the player object using its tag
        playerObject = GameObject.FindWithTag("PlayerTag");

        if (playerObject == null)
        {
            Debug.LogError("Player object with tag 'PlayerTag' not found!");
            return;
        }

        // Store the player's original position
        originalPlayerPosition = playerObject.transform.position;

        MainMenuPanel.SetActive(true);
        IsHidden = false;
    }

    void Update()
    {
        Unhide();
    }

    public void Hide()
    {
        if (playerObject != null)
        {
            // Move player to a new position
            playerObject.transform.position = new Vector3(-117, -0.2959347f, 0);
        }

        // Hide UI
        MainMenuPanel.SetActive(false);
        IsHidden = true;
    }

    public void Unhide()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if (playerObject != null)
            {
                // Restore player's original position
                playerObject.transform.position = originalPlayerPosition;
            }

            MainMenuPanel.SetActive(true);
            IsHidden = false;

            Debug.Log("Panel Should Unhide");
        }
    }
}
