using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject firstStep;
    public GameObject secondStep;


    public int tutorialActive = -1;

    // Use this for initialization
    void Start() {
        tutorialActive = PlayerPrefs.GetInt("TutorialTrigger", 0);

        if(tutorialActive == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
            firstStep.SetActive(true);
        }
    }

    public void CloseTut(int first = 0)
    {
        if(first == 1)
        {
            firstStep.SetActive(false);
            secondStep.SetActive(true);
            
        }
        else
        {
            PlayerPrefs.SetInt("TutorialTrigger", 1);
            secondStep.SetActive(false);
            Time.timeScale = 1;
            transform.GetChild(0).gameObject.SetActive(false);
        }
   
    }

}
