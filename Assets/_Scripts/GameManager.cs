
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

   
    public bool PowerUpEnabled = false;
    public GameObject powerUpPanel;
    public GameObject PowerUpImg;
    public GameObject PowerUpMissileImg;
    public int scoreValue = 10;

    public bool gameOver=false;
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
    public bool moneyUp = false;

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
    public Image titlePlanet;
    public Transform earth;
    public GameObject backGround;
    public GameObject star;
    public GameObject frontLight;
    public GameObject starExplosion;
    public GameObject erisShip;
    public Text briefing;

    void Start()
    {


        /*INITIALIZER==========================================*/
        missileLimit = 1;
        PowerUpEnabled = false;
        moneyUp = false;
        shieldUp = false;

        if (SceneManager.GetActiveScene().name == "Main")
        {
           

            AudioManager.Instance.PlaySound("mainMusic",true);
            //pick earth location
            earth = tmpPlanets[PlayerPrefs.GetInt("EarthIndex", 1)].transform;
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
        else if (SceneManager.GetActiveScene().name == "TITLE")
        {
            /******INTRO BRIEFING INITIALIZER****/
            #region titleString
            string planetName;
            string two;
            string three;
            string four;
            string five;

            int year;
            if (PlayerPrefs.GetInt("LevelIndex", 0) != 0)
            {
                planetName = PlayerPrefs.GetString("PlanetName", "Earth");
                year = PlayerPrefs.GetInt("Year", 2030);
                year = year + Random.Range(1000, 2000);
                PlayerPrefs.SetInt("Year", year);
                two = "once more";
                three = "again";
                four = "repair";
                five = " .... again";

            }
            else
            {
                planetName = "Earth";
                year = 2030;
                two = "";
                three = "";
                four = "build";
                five = "";
            }


            briefing.text = string.Format("Planet <color=green>{0}</color>, year {1}. " +
                "Mankind has discovered a suspicious activity in space {2}. Unseen asteroid flow is threatening species survival. Civilization has united {3} to " +
                "build a spaceship and set sails to another part of galaxy. The only way to succeed is to gather rare compounds " +
                "from asteroids to {4} a colony ship. Help humanity stay alive {5}.", planetName, year.ToString(), two, three,four,five);
            #endregion stringManager;

            /*******INTRO BRIEFING ******/
            AudioManager.Instance.PlaySound("titleMusic",true);
        }
        else if (SceneManager.GetActiveScene().name == "CREDITS")
        {

            /******INTRO BRIEFING INITIALIZER****/
            #region creditString

            List<string> actors = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                actors.Add(LevelManager.Instance.actorNamesPrefs[Random.Range(0, LevelManager.Instance.actorNamesPrefs.Length)]);
            }

           
           

            briefing.text = string.Format("Directed by\nSolid Hinken\n\n" +
                "Story\nSolid Hinken\n\n" +
                "Music\nDL-sounds\nMarcin Klosowski\n\n" +
                "Cast\n{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}\n\n" +
                "Director of photography\nSolid Hinken\n\n" +
                "Camera\nSolid Hinken\n\n\n\n\nTo Be Continued....", actors[0], actors[1], actors[2],actors[3],actors[4]);
            #endregion creditString

            /*******INTRO BRIEFING ******/
            AudioManager.Instance.PlaySound("creditsMusic",true);
        }





        //Grab logoPlanet 
        if (titlePlanet != null)
            titlePlanet.sprite = LevelManager.Instance.lvlData.logoPlanet;

        //Grab star Explosion color
        if (starExplosion != null)
            starExplosion.GetComponent<Renderer>().material.SetColor("_TintColor", LevelManager.Instance.lvlData.starColor);

        //Grab whether ship is there or not
        if (erisShip != null && PlayerPrefs.GetInt("LevelIndex", 0) == 0)
            erisShip.SetActive(false);
        else if (erisShip != null)
            erisShip.SetActive(true);

        //Grab Earth material
        earth.GetChild(1).GetComponent<Renderer>().material = LevelManager.Instance.lvlData.earthMat;

        //Grab backGround
        backGround.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.lvlData.backGround;

        //Check if it's grey and has color
        int backGrndCheck = PlayerPrefs.GetInt("BackColor", 0);
        if (backGrndCheck != 0)
        {
            backGround.GetComponent<SpriteRenderer>().color = LevelManager.Instance.backColors[backGrndCheck];
            //Reset color index again
            PlayerPrefs.SetInt("BackColor", 0);
        }
        //Grab StarColor
        star.GetComponent<Star>().baseStarColor = LevelManager.Instance.lvlData.starColor;

        ////Grab light color
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
