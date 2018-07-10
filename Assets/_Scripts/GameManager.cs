using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    public bool gameOver=false;

    public float money = 0;
    public float scores=0;
    public float mps=10;
    public float moneyTimer;


    public float missileLimit = 1;
    public float missileTime = 2;

    public bool shieldUp = false;
    public GameObject shieldUI;
    public GameObject timeUI;

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
            else
            {
                    GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");
                    lifeCount = lifes.Length;
                    return;
            }

            
          
        }
    }

    public float lifeTimer;
    public float lifeSpreadTime;
    public int lifeIndex = 0;
    public Slider lifeSlider;

    //public GameObject solarSystem;
    public int numberOfPlanets = 3;
    public Queue<GameObject> lifePlanets;
    public List<GameObject> tmpLives;

    private void Start()
    {
        lifePlanets = new Queue<GameObject>();

       

        foreach (GameObject planet in tmpLives)
        {
            lifePlanets.Enqueue(planet);
        }
        //Debug.Log(lifePlanets.Count);

        Time.timeScale = 1f;
        moneyMultiplier.text = string.Format("x{0}", lifeCount);
        moneyText.text = money.ToString();
        scoreText.text = scores.ToString();
    }


    // Update is called once per frame
    void FixedUpdate() {

        moneyTimer += Time.deltaTime;

        //Debug.Log(lifePlanets.Count);

        if (moneyTimer >= 1)
        {
            money += mps * lifeCount;
           // Debug.Log(">>>>" + mps * lifeCount);

            moneyText.text = money.ToString();
            moneyTimer = 0;
        }

        if (lifeCount < numberOfPlanets)
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
            //if(lifeMultiplier != 1)
            //{
            //    lifeSpreadTime *= 1.25;
            //    lifeMultiplier = 1;
            //}



            if (lifeCount < numberOfPlanets)
            {
                GameObject lifeTmp;
                do
                {
                    if (lifePlanets.Count > 0)
                    {
                        lifeTmp = lifePlanets.Dequeue();
                        //Debug.Log("do " + lifePlanets.Count + " : " + lifeTmp.CompareTag("Life"));
                    }
                    else lifeTmp = null;
                }
                while (lifeTmp == null && lifeTmp.CompareTag("Life"));


                lifeTmp.GetComponent<Planet>().SetTag = "Life";
                lifeTmp.GetComponent<Outline>().enabled = true;


                lifeTimer = 0;
                lifeSpreadTime += 0.25f * lifeSpreadTime;

                if (shieldUp)
                    lifeTmp.transform.GetChild(0).gameObject.SetActive(true);

            }
            LifeCount++;
            moneyMultiplier.text = string.Format("x{0}", lifeCount);
        }



        //if(timeUI.activeSelf == true)
        //{
        //    timeUI.GetComponent<Image>().fillAmount = 1-AsteroidSpawner.Instance.lifeSlider.value;
        //}
	}



    public IEnumerator StopGameOver()
    {
        gameOver = true;
        yield return new WaitForSecondsRealtime(1f);
        FunctionHandler.Instance.OpenMenu();
        Time.timeScale = 0f;
    }
}
