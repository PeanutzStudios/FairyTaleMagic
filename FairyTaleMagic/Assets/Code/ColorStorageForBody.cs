using UnityEngine;

public class bodyColorStorage : MonoBehaviour
{
    public static bodyColorStorage Instance;

    private const string ColorKey = "SavedBodyColor";  // Use a unique key for body color
    public Color SelectedColor { get; private set; } = Color.white;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadColor();
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

        ApplyColorToTaggedObjects(color);
    }

    public void LoadColor()
    {
        float r = PlayerPrefs.GetFloat(ColorKey + "_R", 1f);
        float g = PlayerPrefs.GetFloat(ColorKey + "_G", 1f);
        float b = PlayerPrefs.GetFloat(ColorKey + "_B", 1f);
        float a = PlayerPrefs.GetFloat(ColorKey + "_A", 1f);

        SelectedColor = new Color(r, g, b, a);
        ApplyColorToTaggedObjects(SelectedColor);
    }

    private void ApplyColorToTaggedObjects(Color color)
    {
        GameObject[] bodyObjects = GameObject.FindGameObjectsWithTag("Body");

        foreach (var obj in bodyObjects)
        {
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }
    }
}
