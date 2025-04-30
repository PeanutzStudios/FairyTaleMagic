using UnityEngine;
using TMPro; // Required for TextMeshPro InputField

public class TextSaver : MonoBehaviour
{
    public TMP_InputField textInput; // Assign this in the Inspector
    private const string SaveKey = "SavedText"; // Unique key for PlayerPrefs

    void Start()
    {
        // Load saved text if it exists
        if (PlayerPrefs.HasKey(SaveKey))
        {
            textInput.text = PlayerPrefs.GetString(SaveKey);
        }

        // Listen for text changes and save automatically
        textInput.onValueChanged.AddListener(SaveText);
    }

    void SaveText(string newText)
    {
        PlayerPrefs.SetString(SaveKey, newText);
        PlayerPrefs.Save(); // Ensures data is written immediately
    }
}
