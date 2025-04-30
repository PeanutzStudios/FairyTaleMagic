using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PersistentCharacter : MonoBehaviour
{
    public static PersistentCharacter Instance { get; private set; }
    
    private Dictionary<string, string> savedSprites = new Dictionary<string, string>();
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "spriteSave.json");
            LoadSprites();
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public void SaveSprite(string prefabName, string spriteName)
    {
        savedSprites[prefabName] = spriteName;
        SaveSpritesToFile();
    }

    public string GetSavedSprite(string prefabName)
    {
        return savedSprites.ContainsKey(prefabName) ? savedSprites[prefabName] : null;
    }

    private void SaveSpritesToFile()
    {
        string json = JsonUtility.ToJson(new SpriteSaveData { sprites = savedSprites });
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadSprites()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SpriteSaveData data = JsonUtility.FromJson<SpriteSaveData>(json);
            savedSprites = data.sprites ?? new Dictionary<string, string>();
        }
    }

    [System.Serializable]
    private class SpriteSaveData
    {
        public Dictionary<string, string> sprites;
    }
}
