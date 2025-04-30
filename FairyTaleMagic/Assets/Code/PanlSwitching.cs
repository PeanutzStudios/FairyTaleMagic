using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitching : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject MainPanel;
    public GameObject Character;

    public GameObject ProfilePanel;

    public GameObject OnlinePanel;

    public GameObject HairPannel;

    public Button settingsButton;

    public GameObject MainMenuButtonNavi;
    public Button menuButton;


    void Start()
    {
        SettingsPanel.SetActive(false);
        MainPanel.SetActive(true);
        Character.SetActive(true);
        ProfilePanel.SetActive(false);
        HairPannel.SetActive(false);
OnlinePanel.SetActive(false);
MainMenuButtonNavi.SetActive(true);
  
    }



public void hairPanel() {

HairPannel.SetActive(true);
   SettingsPanel.SetActive(false);
       ProfilePanel.SetActive(false);
        MainPanel.SetActive(false);
        MainMenuButtonNavi.SetActive(false);
       
        OnlinePanel.SetActive(false);

}

    public void SettingsButton()
    {
        SettingsPanel.SetActive(true);
          ProfilePanel.SetActive(false);
          HairPannel.SetActive(false);
        MainPanel.SetActive(false);
        MainMenuButtonNavi.SetActive(true);
       
       
        OnlinePanel.SetActive(false);
    }

    public void MenuButton()
    {
        SettingsPanel.SetActive(false);
         ProfilePanel.SetActive(false);
          HairPannel.SetActive(false);
        MainPanel.SetActive(true);
        MainMenuButtonNavi.SetActive(true);
       
        
       
        OnlinePanel.SetActive(false);

    }


    
    public void OnlineButton()
    {
        SettingsPanel.SetActive(false);
         ProfilePanel.SetActive(false);
          HairPannel.SetActive(false);
        MainPanel.SetActive(false);
        MainMenuButtonNavi.SetActive(true);
       
        OnlinePanel.SetActive(true);


    }

public void profilebuttonclosed() {

 ProfilePanel.SetActive(false);

}
      public void ProfileButton()
    {
        SettingsPanel.SetActive(false);
         ProfilePanel.SetActive(true);
        MainPanel.SetActive(true);
         HairPannel.SetActive(false);
         MainMenuButtonNavi.SetActive(true);
   
        OnlinePanel.SetActive(false);

    }

 public void BackButtonPanel() {

        MainPanel.SetActive(true);
         MainMenuButtonNavi.SetActive(true);
        HairPannel.SetActive(false);
    }
}
