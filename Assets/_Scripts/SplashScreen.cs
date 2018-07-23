using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 3.0f; //Minimum logo time;
    public GameObject solidHinken;
    public string nextScene;

    private void Start()
    {

        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 0.99f;


        //Get a timestep of a completion time
        if (Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;




    }


    private void Update()
    {
        //FadeIn
        if (Time.time < minimumLogoTime)
             fadeGroup.alpha = 1 - Time.time;
        //FadeOut
        if (Time.time > minimumLogoTime && loadTime != 0)
        {
            //solidHinken.SetActive(false);
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if (fadeGroup.alpha>=1)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
            


    }
}
