using UnityEngine;
using UnityEngine.UI;

public class HairMenuController : MonoBehaviour
{
    private void Start()
    {
        // Apply the saved color to UI hair previews
        ApplyColorToUIHairPreviews(HairColorStorage.Instance.SelectedColor);
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
