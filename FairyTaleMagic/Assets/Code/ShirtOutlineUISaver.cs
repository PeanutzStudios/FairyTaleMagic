using UnityEngine;
using UnityEngine.UI;

public class ShirtoutlineUiStorage : MonoBehaviour
{
    private void Start()
    {
        // Apply the saved color to UI hair previews
        ApplyColorToUIHairPreviews(ShirtOutlineColorStorage.Instance.SelectedColor);
    }

    private void ApplyColorToUIHairPreviews(Color color)
    {
        GameObject[] uiHairPreviews = GameObject.FindGameObjectsWithTag("ShirtOutline");
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
