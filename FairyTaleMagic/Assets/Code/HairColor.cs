using UnityEngine;
using UnityEngine.UI; // Required for working with UI Images

public class HairColorStorage : MonoBehaviour
{
    public static HairColorStorage Instance;

    private const string ColorKey = "HairSavedColor";
    public Color SelectedColor { get; private set; } = Color.white;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadColor(); // Load saved color on start
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveColor(Color color)
    {
        SelectedColor = color;
        PlayerPrefs.SetFloat(ColorKey + "_R", color.r);
        PlayerPrefs.SetFloat(ColorKey + "_G", color.g);
        PlayerPrefs.SetFloat(ColorKey + "_B", color.b);
        PlayerPrefs.SetFloat(ColorKey + "_A", color.a);
        PlayerPrefs.Save();

        ApplyColorToAllHair(color);
    }

    public void LoadColor()
    {
        float r = PlayerPrefs.GetFloat(ColorKey + "_R", 1f);
        float g = PlayerPrefs.GetFloat(ColorKey + "_G", 1f);
        float b = PlayerPrefs.GetFloat(ColorKey + "_B", 1f);
        float a = PlayerPrefs.GetFloat(ColorKey + "_A", 1f);
        SelectedColor = new Color(r, g, b, a);

        ApplyColorToAllHair(SelectedColor);  // Make sure to apply to both sprite and UI
    }

    public void ApplyColorToAllHair(Color color)
    {
        ApplyColorToTaggedObjects(color);
        ApplyColorToUIHairPreviews(color);
    }

    private void ApplyColorToTaggedObjects(Color color)
    {
        GameObject[] hairObjects = GameObject.FindGameObjectsWithTag("frontHairr");
        foreach (var obj in hairObjects)
        {
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }
    }

    private void ApplyColorToUIHairPreviews(Color color)
    {
        GameObject[] uiHairPreviews = GameObject.FindGameObjectsWithTag("frontHairr");
        foreach (var obj in uiHairPreviews)
        {
            var image = obj.GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }
    }
}
