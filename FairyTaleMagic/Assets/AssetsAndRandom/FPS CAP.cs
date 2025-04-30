using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCAP : MonoBehaviour
{
    private float FPS;

    public enum limits
    {
        // FPS CAP List
        Limit30 = 30,
        Limit15 = 15,
        Limit60 = 50,
       

    }

    public limits limit;

    void Awake()
    {
        // FPS CAP Initioalizer
        Application.targetFrameRate = (int)limit;
    }


    public void fps(float fps)
    {
        // FPS LADER
        FPS = fps;
    }
}
