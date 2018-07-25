
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    


    public bool gameOver=false;
    public Text moneyText;
    public Text moneyGoalTxt;

    public Slider moneySlider;
    public Animator launchAnim;

    public float moneyGoal;
    private float money = 0;
    public float Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
            moneySlider.value = money / moneyGoal;
            moneyGoalTxt.text = string.Format("{0}/{1}", money.ToString(), moneyGoal.ToString());
            if (money >= moneyGoal)
            {
                launchAnim.SetBool("CanLaunch", true);
            }

                //    int lvlIndex = PlayerPrefs.GetInt("LevelIndex", 0);
                //    lvlIndex++;
                //    Debug.Log(lvlIndex);
                //    PlayerPrefs.SetInt("LevelIndex", lvlIndex);
                //    Debug.Log(PlayerPrefs.GetInt("LevelIndex", -1));
                //    PlayerPrefs.SetFloat("MoneyGoal", moneyGoal + 10);

                //    //StartCoroutine(MenuScreen.Instance.FadeOut("Credits"));
                //}
            }
    }

    

    //public float scores = 0;
    //public float mps=10;
    //public float moneyTimer;


    public float missileLimit = 1;
    public float missileTime = 2;

    public bool shieldUp = false;
    public GameObject shieldUI;
    public GameObject timeUI;

   

    //public Text scoreMultip;
    //public Text scoreText;
    
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


    //public GameObject solarSystem;
    public int numberOfPlanets = 3;
    //public List<GameObject> lifePlanets;
    public List<GameObject> tmpPlanets;


    //From levelManager
    public Transform earth;
    public GameObject backGround;
    public GameObject star;
    public GameObject frontLight;


    void Start()
    {
     

        /*INITIALIZER==========================================*/
        if (SceneManager.GetActiveScene().name == "Main")
        {
            //pick earth location
            earth = tmpPlanets[PlayerPrefs.GetInt("EarthIndex",1)].transform;
            //enable moon
            earth.GetChild(2).gameObject.SetActive(true);
            if (earth.GetComponent<Outline>() != null)
                earth.gameObject.GetComponent<Outline>().enabled = true;

            earth.gameObject.tag = "Life";

            //Set venus and mars colors
            tmpPlanets[0].transform.GetChild(1).GetComponent<Renderer>().material.color = LevelManager.Instance.planetColors[PlayerPrefs.GetInt("VenusColor", 0)];
            tmpPlanets[0].transform.GetChild(1).GetComponent<Renderer>().material.color = LevelManager.Instance.planetColors[PlayerPrefs.GetInt("MarsColor", 0)];
            //Money goal stuff
            moneyGoal = PlayerPrefs.GetFloat("MoneyGoal", 50);
            moneyGoalTxt.text = string.Format("{0}/{1}", money.ToString(), moneyGoal.ToString());
            moneySlider.value = 0;
            Time.timeScale = 1f;
        }


        earth.GetChild(1).GetComponent<Renderer>().material = LevelManager.Instance.lvlData.earthMat;
        backGround.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.lvlData.backGround;
        star.GetComponent<Star>().baseStarColor = LevelManager.Instance.lvlData.starColor;
        //frontLight.GetComponent<Light>().color = LevelManager.Instance.lvlData.starColor;

        /*INITIALIZER==========================================*/



       
     
    }


 //   // Update is called once per frame
 //   void FixedUpdate() {



 //       //if (shieldUp)
            
            


 //       //moneyTimer += Time.deltaTime;

 //       //Debug.Log(lifePlanets.Count);

 //       //if (moneyTimer >= 1)
 //       //{
 //       //    scores += mps * lifeCount;
 //       //   // Debug.Log(">>>>" + mps * lifeCount);

 //       //    scoreText.text = scores.ToString();
 //       //    moneyTimer = 0;
 //       //}







 //       //if (lifeCount < numberOfPlanets)
 //       //{
 //       //    //lifeTimer += 1 *  Time.deltaTime;
 //       //    lifeSlider.value = lifeTimer / lifeSpreadTime;
 //       //}
 //       //else
 //       //{
 //       //    lifeTimer = 0;
 //       //    lifeSlider.value = lifeTimer / lifeSpreadTime;
 //       //}
 //       //if (lifeTimer>= lifeSpreadTime )
 //       //{

 //       //    lifeTimer = 0;
 //       //    //if(lifeMultiplier != 1)
 //       //    //{
 //       //    //    lifeSpreadTime *= 1.25;
 //       //    //    lifeMultiplier = 1;
 //       //    //}

 //       //if (lifeCount < numberOfPlanets)
 //       //    {
 //       //        GameObject lifeTmp;
 //       //        do
 //       //        {
 //       //            if (lifePlanets.Count > 0)
 //       //            {
 //       //                lifeTmp = lifePlanets.Dequeue();
 //       //                //Debug.Log("do " + lifePlanets.Count + " : " + lifeTmp.CompareTag("Life"));
 //       //            }
 //       //            else lifeTmp = null;
 //       //        }
 //       //        while (lifeTmp == null && lifeTmp.CompareTag("Life"));


 //       //        lifeTmp.GetComponent<Planet>().SetTag = "Life";
 //       //        lifeTmp.GetComponent<Outline>().enabled = true;


 //       //        lifeTimer = 0;
 //       //        lifeSpreadTime += 0.25f * lifeSpreadTime;

 //       //        if (shieldUp)
 //       //            lifeTmp.transform.GetChild(0).gameObject.SetActive(true);

 //       //    }
 //           //LifeCount++;
 //           //scoreMultip.text = string.Format("x{0}", lifeCount);
 //       //}



 //       //if(timeUI.activeSelf == true)
 //       //{
 //       //    timeUI.GetComponent<Image>().fillAmount = 1-AsteroidSpawner.Instance.lifeSlider.value;
 //       //}
	//}



    public IEnumerator StopGameOver()
    {
        gameOver = true;
        yield return new WaitForSecondsRealtime(1f);
        FunctionHandler.Instance.OpenMenu();
        Time.timeScale = 0f;
    }
}
