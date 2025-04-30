using UnityEngine;

public class DontKill : MonoBehaviour
{

    public static DontKill Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if (Instance == null)
{
    Instance = this;
    DontDestroyOnLoad(gameObject);
}
else
{
    Destroy(gameObject); // Prevents duplicates
}

    }
}
