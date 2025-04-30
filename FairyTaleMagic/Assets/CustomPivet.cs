using UnityEngine;

public class CustomPivot : MonoBehaviour
{
    public GameObject parentObject;  // The empty GameObject you're using as the parent (pivot)
    public Vector2 pivotOffset;      // The custom offset you want for the new pivot point

    void Start()
    {
        // Parent the object to the new parent
        transform.SetParent(parentObject.transform);

        // Apply the offset to maintain the visual position relative to the new pivot
        Vector3 offset = new Vector3(pivotOffset.x, pivotOffset.y, 0);
        transform.localPosition = offset;
    }
}
