using UnityEngine;
using UnityEngine.UI;

public class ArmR : MonoBehaviour
{
    public static ArmR Instance; // Singleton for hand controller
     // Reference to the hand prefab (but NOT used for rotation)
    public Slider slider; // Reference to the slider controlling the rotation

    private GameObject handInstance; // Reference to the actual hand object in the scene
    private float targetRotation = 0f; // The target rotation for smooth transition

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Find the hand object in the scene instead of using the prefab reference
        handInstance = GameObject.FindWithTag("ArmR");

        if (handInstance == null)
        {
            Debug.LogError("Hand instance not found in the scene! Ensure the object exists and has the 'Hand' tag.");
            return;
        }

        if (slider == null)
        {
            Debug.LogError("Slider is not assigned in the Inspector!");
            return;
        }

        slider.onValueChanged.AddListener(OnSliderValueChanged);
        targetRotation = slider.value * 180f; // Initialize rotation
    }

    private void OnSliderValueChanged(float value)
    {
        // Update target rotation based on slider value
        targetRotation = value * 360f;
        Debug.Log("Slider Changed: " + value + " | Target Rotation: " + targetRotation);
    }

    void Update()
    {
        if (handInstance != null)
        {
            // Smoothly rotate the hand GameObject towards the target rotation
            float currentRotation = handInstance.transform.localEulerAngles.z;
            float smoothRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * 10f);

            // Apply the smooth rotation to the hand GameObject
            handInstance.transform.localEulerAngles = new Vector3(0f, 0f, smoothRotation);
        }
    
    }
}
