using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public bool gameOver=false;

    public float money = 0;
    public float scores=0;
    public float mps=1;
    public float moneyTimer;


    public float missileLimit = 1;

    public Text moneyText;
    public Text scoreText;
    public Text moneyMultiplier;
    
    public GameObject menu;
    public GameObject ui;



    public float missileCost;
    public GameObject missile;

    public float lifeMultiplier = 1;
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
                StartCoroutine(StopGameOver());
                
            }

            if (lifeCount < lifeIndex && lifeCount >= 0)
            {
                    lifeIndex--;
                   
                    if (lifeIndex < 0)
                        lifeIndex = lifePlanets.Count;
            }
            else if (lifeCount > lifeIndex && lifeCount<=lifePlanets.Count)
            {
                while (lifeIndex >= 0 && lifeIndex < lifePlanets.Count && lifePlanets[lifeIndex].GetComponent<Outline>().enabled != false)
                {
                    lifeIndex++;
                    if (lifeIndex >= lifePlanets.Count)
                        lifeIndex = 0;
                }

                lifePlanets[lifeIndex].GetComponent<Planet>().SetTag = "Life";
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
        moneyMultiplier.text = string.Format("x{0}", lifeCount);
        moneyText.text = money.ToString();
        scoreText.text = scores.ToString();
    }


    // Update is called once per frame
    void FixedUpdate() {

        moneyTimer += Time.deltaTime;

        if (moneyTimer >= 1)
        {
            money += mps * lifeCount;
            moneyText.text = money.ToString();
            moneyTimer = 0;
        }

        if (lifeCount < lifePlanets.Count)
        {
            lifeTimer += 1 *  Time.deltaTime;
            lifeSlider.value = lifeTimer / lifeSpreadTime;
        }
        else
        {
            lifeTimer = 0;
            lifeSlider.value = lifeTimer / lifeSpreadTime;
        }
        if (lifeTimer>= lifeSpreadTime )
        {
            
            lifeTimer = 0;
            if(lifeMultiplier != 1)
            {
                lifeSpreadTime *= 2;
                lifeMultiplier = 1;
            }
            LifeCount++;
            moneyMultiplier.text = string.Format("x{0}", lifeCount);
        }
	}



    public IEnumerator StopGameOver()
    {
        gameOver = true;
        yield return new WaitForSecondsRealtime(0.5f);
        FunctionHandler.Instance.OpenMenu();
        Time.timeScale = 0f;
    }
}
