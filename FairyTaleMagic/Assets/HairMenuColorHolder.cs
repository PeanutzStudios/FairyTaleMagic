using UnityEngine;
using UnityEngine.UI;

public class HairOutlineMenuController : MonoBehaviour
{
    private void Start()
    {
        // Apply the saved color to UI hair previews
        ApplyColorToUIHairPreviews(FrontHairOutlineStorage.Instance.SelectedColor);
    }

    private void ApplyColorToUIHairPreviews(Color color)
    {
        GameObject[] uiHairPreviews = GameObject.FindGameObjectsWithTag("frontHairoutline");
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
