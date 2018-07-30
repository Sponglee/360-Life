
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

            switch (Random.Range(0,2))
            {
                case 0:
                    AudioManager.Instance.PlaySound("mainMusic", true);
                    break;
                case 1:
                    AudioManager.Instance.PlaySound("mainMusic1", true);
                    break;
                case 2:
                    AudioManager.Instance.PlaySound("mainMusic2", true);
                    break;
                default:
                    break;
            }
          





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

            AudioManager.Instance.PlaySound("titleMusic", true);





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
                "Mankind has discovered a suspicious activity in space {2}. Unseen asteroid flow is threatening our survival. Whole civilization has united {3} to " +
                "{4} a spaceship and set sails to another part of the galaxy. The only way to succeed is to gather rare compounds " +
                "from asteroids to {4} a colony ship. Help humanity stay alive {5}.", planetName, year.ToString(), two, three,four,five);
            #endregion stringManager;

            /*******INTRO BRIEFING ******/



        }
        else if (SceneManager.GetActiveScene().name == "CREDITS")
        {
            AudioManager.Instance.PlaySound("creditsMusic", true);





            /******INTRO BRIEFING INITIALIZER****/
            #region creditString

            List<string> actors = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                actors.Add(LevelManager.Instance.actorNamesPrefs[Random.Range(0, LevelManager.Instance.actorNamesPrefs.Length)]);
            }

           
           

            briefing.text = string.Format(
                "DIRECTED BY\nSolid Hinken\n\n" +
                "STORY\nSolid Hinken\n\n" +
                "GAME DESIGN\nSolid Hinken\n\n" +
               
                "MUSIC\nDL-sounds\nMarcin Klosowski\nMISO STUDIO\n\n\n" +
                "GUEST ACTORS\n{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}\n\n" +
                 "PROGRAMMING\nSolid Hinken\n\n" +
                "ANIMATION\nSolid Hinken\n\n" +
                "CAMERA\nSolid Hinken\n\n\n\n\n\n\n\nTo Be Continued....", actors[0], actors[1], actors[2],actors[3],actors[4]);
            #endregion creditString

            /*******INTRO BRIEFING ******/
           
        }





        //Grab logoPlanet 
        if (titlePlanet != null)
            titlePlanet.sprite = LevelManager.Instance.lvlData.logoPlanet;

        //Get a sequel number 
        if (PlayerPrefs.GetInt("LevelIndex", 0) > 0 && SceneManager.GetActiveScene().name == "TITLE")
            GameObject.FindGameObjectWithTag("titleNumber").GetComponent<Text>().text = (PlayerPrefs.GetInt("LevelIndex", 0) + 1).ToString();
        
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


    public IEnumerator StopGameOver()
    {
        gameOver = true;
        yield return new WaitForSecondsRealtime(1f);
        FunctionHandler.Instance.OpenMenu();
        Time.timeScale = 0f;
    }
}
