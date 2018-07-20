using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : Singleton<MenuScreen> {


    [SerializeField]
    private CanvasGroup fadeGroup;

    private float fadeInSpeed = 0.33f;


	// Use this for initialization
	private void Start ()
    {
        gameObject.SetActive(true);
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update () {
        //FadeIn
        if(fadeGroup.alpha > 0)
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad;
    }



    public IEnumerator FadeOut(string sceneName)
    {
        while(fadeGroup.alpha < 1)
        {
            fadeGroup.alpha = 1 + Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        
    }
}
