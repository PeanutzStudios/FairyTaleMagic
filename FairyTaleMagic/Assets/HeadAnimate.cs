using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeadAnimate : MonoBehaviour
{


    public Slider slider;

    
    public GameObject HeadPiviot;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.GetComponent<Slider>();
    }

   public void slidervalue() {

    Animator HeadRoation = GetComponent<Animator>();

    HeadRoation.SetFloat("HeadPiviot", slider.value);
   }
}
