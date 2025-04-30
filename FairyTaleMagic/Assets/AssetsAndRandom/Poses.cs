using UnityEngine;

public class OutfitFollowPlayer : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject shirtPrefab; // Reference to the shirt prefab
    public GameObject skirtPrefab; // Reference to the skirt prefab

    // Offsets to position the outfits relative to the player's body (for snapping to exact positions)
    public Vector3 shirtOffset = new Vector3(0f, 1.5f, 0f); // Position for shirt above the player
    public Vector3 skirtOffset = new Vector3(0f, -1.5f, 0f); // Position for skirt below the player

    void Start()
    {
        // Parent the outfits to the player, so they follow when the player moves
        AttachOutfitsToPlayer();
        
        // Optionally, initialize their positions when the scene starts
        UpdateOutfitPositions();
    }

    void Update()
    {
        // Continuously update the outfit positions every frame (in case player moves)
        UpdateOutfitPositions();
    }

    // Method to parent outfits to the player
    void AttachOutfitsToPlayer()
    {
        if (shirtPrefab != null)
        {
            shirtPrefab.transform.SetParent(player.transform); // Make the shirt a child of the player
        }

        if (skirtPrefab != null)
        {
            skirtPrefab.transform.SetParent(player.transform); // Make the skirt a child of the player
        }
    }

    // Method to update the positions of the outfits based on the player's movement
    void UpdateOutfitPositions()
    {
        if (shirtPrefab != null)
        {
            // Update the position of the shirt to be relative to the player
            shirtPrefab.transform.localPosition = shirtOffset;
        }

        if (skirtPrefab != null)
        {
            // Update the position of the skirt to be relative to the player
            skirtPrefab.transform.localPosition = skirtOffset;
        }
    }
}
