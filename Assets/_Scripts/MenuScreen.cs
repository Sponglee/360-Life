using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : Singleton<MenuScreen> {


    [SerializeField]
    private CanvasGroup fadeGroup;

    private float fadeInSpeed = 0.33f;
    private bool isFadingOut = false;

    //for tutorial timeFreeze
    private bool bringTimeBack = false;

    // Use this for initialization
    private void Start ()
    {
        isFadingOut = false;
        gameObject.SetActive(true);
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update () {
        //FadeIn
        if (fadeGroup.alpha > 0 && !isFadingOut)
        {
            if (Time.timeScale == 0)
            {
                bringTimeBack = true;
                Time.timeScale = 1;
            }
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad;
        }
        else if(bringTimeBack && SceneManager.GetActiveScene().name == "Main")
        {
            Time.timeScale = 0;
            bringTimeBack = false;
        }
       
    }



    public IEnumerator FadeOut(string sceneName)
    {
        isFadingOut = true;
        while(fadeGroup.alpha < 1)
        {
            fadeGroup.alpha = 1 + Time.fixedUnscaledDeltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        
    }
}
