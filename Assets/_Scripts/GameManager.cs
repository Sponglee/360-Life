using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public bool gameOver=false;


    public float scores=0;
    public float spm=1;

    public Text scoreText;
    public Text scoreMultiplier;
    
    public GameObject menu;
    public GameObject ui;


   
    [SerializeField]
    private int lifeCount = 1;
    public int LifeCount
    {
        get {return lifeCount;}
        set
        {
            lifeCount = value;
            if (LifeCount<=0)
            {
                gameOver = true;
                FunctionHandler.Instance.OpenMenu();
                Time.timeScale = 0f;
                
            }

            if (lifeCount < lifeIndex && lifeCount >= 0)
            {
                //Debug.Log(lifeIndex + "   :   " + lifeCount);
                //while (lifeIndex >= 0 && lifeIndex<lifePlanets.Count && lifePlanets[lifeIndex].GetComponent<Outline>().enabled != true)
                //{

                    lifeIndex--;
                   
                    if (lifeIndex < 0)
                        lifeIndex = lifePlanets.Count;
                    //}

               


            }
            else if (lifeCount > lifeIndex)
            {
                while (lifeIndex >= 0 && lifeIndex < lifePlanets.Count && lifePlanets[lifeIndex].GetComponent<Outline>().enabled != false)
                {
                    lifeIndex++;
                    if (lifeIndex >= lifePlanets.Count)
                        lifeIndex = 0;
                }

                lifePlanets[lifeIndex].tag = "Life";
                lifePlanets[lifeIndex].GetComponent<Outline>().enabled = true;


                lifeTimer = 0;
                lifeSpreadTime += 0.5f * lifeSpreadTime;
            }
            else
                return;
          
        }
    }

    public float lifeTimer;
    public float lifeSpreadTime;
    public int lifeIndex = 0;
    public Slider lifeSlider;

    public List<GameObject> lifePlanets;

    private void Start()
    {
        Time.timeScale = 1f;
        scoreMultiplier.text = string.Format("x{0}", lifeCount);
        scoreText.text = scores.ToString();
    }


    // Update is called once per frame
    void FixedUpdate () {

        scores += spm * lifeCount;
        scoreText.text = scores.ToString();
        

        lifeTimer += Time.deltaTime;
        lifeSlider.value = lifeTimer/lifeSpreadTime;

        if (lifeTimer>= lifeSpreadTime)
        {

            LifeCount++;
            scoreMultiplier.text = string.Format("x{0}", lifeCount);
        }
	}
}
