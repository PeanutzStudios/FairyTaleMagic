using Unity.VisualScripting;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public GameObject MainMenuPane;
   public GameObject HairPanel;
   public GameObject Clothing;
   public GameObject Props;

    void Start()
    {
     
        HairPanel.SetActive(false);

    }
    void Update()
    {
        hairPanel();
    }


public void hairPanel() {

MainMenuPane.SetActive(false);
HairPanel.SetActive(true);



}

}
 
