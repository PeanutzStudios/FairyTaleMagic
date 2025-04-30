using JetBrains.Annotations;
using UnityEngine;

public class BackButtonCode : MonoBehaviour
{
 public GameObject MainMenuPanelOpen;
 public GameObject customizationScene;


  
    void Update()
    {
        BackButtonPanel();
    }



    public void BackButtonPanel() {

        MainMenuPanelOpen.SetActive(true);
        customizationScene.SetActive(false);
    }
}
