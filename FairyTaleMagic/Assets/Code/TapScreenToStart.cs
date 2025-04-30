using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TapScreenToStart : MonoBehaviour
{
public string sceneName;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {

            SceneManager.LoadScene(sceneName);
        }
    }
}
