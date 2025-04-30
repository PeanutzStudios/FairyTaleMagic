using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausemenu;
    public Button Button;



    public bool isPuased;
 


    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);
    }

    // Update is called once per frame
  public void PausedButton()
  {

      if (isPuased)
        {
          resumeGame();
        }

        else 
        {
           pauseGame();
        }
  }

public void pauseGame()
{
   pausemenu.SetActive(true);
   Time.timeScale = 0f;
   isPuased = true;
}

public void resumeGame()
{
  pausemenu.SetActive(false);
   Time.timeScale = 1f;
   isPuased = false;
}











}



