using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;    // Reference to the sprite's SpriteRenderer
    public Image previewImage;               // Reference to the UI Image for preview
    public Button applyButton;               // Reference to the Apply button (optional)

    private Color currentColor;              // Stores the current selected color
    private const string colorKey = "selectedColor"; // Key for PlayerPrefs

    // Start is called before the first frame update
    void Start()
    {
        // Load the saved color when the game starts
        currentColor = LoadColor();
        ApplyColor(currentColor);

        // Add the listener to the Apply button if it's set
        if (applyButton != null)
        {
            applyButton.onClick.AddListener(ApplyColorToPrefab);
        }
    }

    // Changes the color of the preview image and the actual sprite
    public void ChangeColor(Color newColor)
    {
        currentColor = newColor;
        previewImage.color = currentColor; // Update preview UI image
    }

    // Apply the selected color to the sprite and save it
    public void ApplyColorToPrefab()
    {
        ApplyColor(currentColor);
        SaveColor(currentColor);
    }

    // Helper function to apply color to the sprite
    private void ApplyColor(Color color)
    {
        spriteRenderer.color = color;
    }

    // Save the selected color to PlayerPrefs
    private void SaveColor(Color color)
    {
        string colorString = ColorUtility.ToHtmlStringRGBA(color);
        PlayerPrefs.SetString(colorKey, colorString);
        PlayerPrefs.Save();
    }

    // Load the saved color from PlayerPrefs
    private Color LoadColor()
    {
        string colorString = PlayerPrefs.GetString(colorKey, "#FFFFFF"); // Default to white if no color is saved.
        Color color;
        ColorUtility.TryParseHtmlString("#" + colorString, out color);
        return color;
    }

    // Method to be called from UI Buttons for each color
    public void OnColorButtonClick(Color newColor)
    {
        ChangeColor(newColor);
    }

    // Add the base colors here for easy access
    public static Color Red => Color.red;
    public static Color Green => Color.green;
    public static Color Blue => Color.blue;
    public static Color Yellow => Color.yellow;
    public static Color Cyan => Color.cyan;
    public static Color Magenta => Color.magenta;
    public static Color Black => Color.black;
    public static Color White => Color.white;
    public static Color Gray => Color.gray;
    public static Color Orange => new Color(1f, 0.647f, 0f);  // Custom orange color
    public static Color Pink => new Color(1f, 0.411f, 0.705f);  // Custom pink color
    public static Color Purple => new Color(0.5f, 0f, 0.5f);  // Custom purple color

    // Light Colors (lighter shades of the base colors)
    public static Color LightRed => LightenColor(Color.red);
    public static Color LightGreen => LightenColor(Color.green);
    public static Color LightBlue => LightenColor(Color.blue);
    public static Color LightYellow => LightenColor(Color.yellow);
    public static Color LightCyan => LightenColor(Color.cyan);
    public static Color LightMagenta => LightenColor(Color.magenta);
    public static Color LightBlack => LightenColor(Color.black);
    public static Color LightWhite => Color.white; // White is already light
    public static Color LightGray => LightenColor(Color.gray);
    public static Color LightOrange => LightenColor(new Color(1f, 0.647f, 0f));  // Light orange
    public static Color LightPink => LightenColor(new Color(1f, 0.411f, 0.705f));  // Light pink
    public static Color LightPurple => LightenColor(new Color(0.5f, 0f, 0.5f));  // Light purple

    // Function to lighten any color by blending it with white
    private static Color LightenColor(Color color)
    {
        return Color.Lerp(color, Color.white, 0.5f);  // Mix the color with white (50% lightened)
    }
}
