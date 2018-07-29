using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject firstStep;
    public GameObject secondStep;
    public GameObject thirdStep;

    public int tutorialActive = -1;

    // Use this for initialization
    void Start() {
        tutorialActive = PlayerPrefs.GetInt("TutorialTrigger", 0);

        if(tutorialActive <= 2)
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
        else if (first == 2)
        {
            secondStep.SetActive(false);
            thirdStep.SetActive(true);

        }
        else
        {
            PlayerPrefs.SetInt("TutorialTrigger",3);
            thirdStep.SetActive(false);
            Time.timeScale = 1;
            transform.GetChild(0).gameObject.SetActive(false);
        }
   
    }

}
