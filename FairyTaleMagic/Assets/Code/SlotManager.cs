using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform gridParent;
    public Button inventoryToggleButton;
    public Button nextPageButton, prevPageButton;
    public int slotsPerPage = 27;

    private int currentPage = 0;
    private int selectedIndex = -1; // Track selected sprite

    private List<GameObject> slots = new List<GameObject>();

    public List<Sprite> colorSprites = new List<Sprite>();
    public GameObject selectedColorPrefab;
    public GameObject selectedOutlinePrefab;
    public List<Sprite> outlineSprites = new List<Sprite>();

    void Start()
    {
        inventoryToggleButton.onClick.AddListener(ToggleInventory);
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PrevPage);
        inventoryPanel.SetActive(false);

        LoadSprite();
        PopulateInventory();
    }

    private void SaveSprite()
    {
        if (selectedIndex >= 0 && selectedIndex < colorSprites.Count)
        {
            PlayerPrefs.SetInt("SelectedSpriteIndex", selectedIndex);
            PlayerPrefs.Save();
            Debug.Log("Saved sprite index: " + selectedIndex);
        }
    }

    private void LoadSprite()
    {
        selectedIndex = PlayerPrefs.GetInt("SelectedSpriteIndex", 0);
        currentPage = selectedIndex / slotsPerPage;

        if (selectedIndex >= 0 && selectedIndex < colorSprites.Count)
        {
            Debug.Log("Loaded sprite index: " + selectedIndex);
        }
        else
        {
            selectedIndex = 0;
        }

        PopulateInventory();
    }

    void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void PopulateInventory()
    {
        // Clear previous slots
        foreach (var slot in slots) Destroy(slot);
        slots.Clear();

        int startIndex = currentPage * slotsPerPage;
        int endIndex = Mathf.Min(startIndex + slotsPerPage, colorSprites.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject slot = Instantiate(slotPrefab, gridParent);
            Button button = slot.GetComponent<Button>();
            Image slotImage = slot.transform.GetChild(0).GetComponent<Image>();

            slotImage.sprite = colorSprites[i];
            int index = i;
            button.onClick.AddListener(() => SelectPrefab(index));

            slots.Add(slot);
        }

        prevPageButton.interactable = currentPage > 0;
        nextPageButton.interactable = (currentPage + 1) * slotsPerPage < colorSprites.Count;
    }

    void SelectPrefab(int index)
    {
        if (index >= 0 && index < colorSprites.Count && index < outlineSprites.Count)
        {
            selectedIndex = index; // Track selected index
            SaveSprite(); // Save selection

            if (selectedColorPrefab != null)
                selectedColorPrefab.GetComponent<SpriteRenderer>().sprite = colorSprites[index];

            if (selectedOutlinePrefab != null)
                selectedOutlinePrefab.GetComponent<SpriteRenderer>().sprite = outlineSprites[index];

            Debug.Log("Selected: " + colorSprites[index].name);
        }
    }

    public void NextPage()
    {
        if ((currentPage + 1) * slotsPerPage < colorSprites.Count)
        {
            currentPage++;
            PopulateInventory();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            PopulateInventory();
        }
    }

    public void Open() => inventoryPanel.SetActive(true);
    public void ClosePanel() => inventoryPanel.SetActive(false);
}
