using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject Panel;

    // Update is called once per frame
    void Start()
    {
        Panel.SetActive(false);
        
    }



    public void Show() {

        Panel.SetActive(true);
    }

     public void closed() {

        Panel.SetActive(false);
    }
}

