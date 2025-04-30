using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScnenTimer : MonoBehaviour
{

    public string sceneName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("NewScene", 3f);

    }

   public void NewScene() {

    SceneManager.LoadScene(sceneName);
   }

}
