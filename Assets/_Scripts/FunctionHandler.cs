using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHandler : Singleton<FunctionHandler>
{


    public GameObject gameOverText;
    public Text menuScoreText;
    public Text waveMenuText;


    //Double Money
    private int moneyMultiplier;
    public Text moneyMultiplierText;
    public Text doubleLifeCostText;


    [SerializeField]
    private float doubleLifeCost;
    public float DoubleLifeCost
    {
        get
        {
            return doubleLifeCost;
        }

        set
        {
            doubleLifeCost = value;
            doubleLifeCostText.text = value.ToString();
        }
    }

    //+1 missile
    private int missileMultiplier;
    public Text missileMultiplierText;
    public Text doubleMissileCostText;


    [SerializeField]
    private float doubleMissileCost;
    public float DoubleMissileCost
    {
        get
        {
            return doubleMissileCost;
        }

        set
        {
            doubleMissileCost = value;
            doubleMissileCostText.text = value.ToString();
        }
    }

    //Double CoolDown
    public Text doubleMissileTimeText;



    [SerializeField]
    private float doubleMissileTime;
    public float DoubleMissileTime
    {
        get
        {
            return doubleMissileTime;
        }

        set
        {
            doubleMissileTime = value;
            doubleMissileTimeText.text = value.ToString();
        }
    }

    //Shield 
    public Text doubleShieldText;
    [SerializeField]
    private float doubleShieldCost;
    public float DoubleShieldCost
    {
        get
        {
            return doubleShieldCost;
        }

        set
        {
            doubleShieldCost = value;
            doubleShieldText.text = value.ToString();
        }
    }

    

    //Shield 
    public Text doubleMoneyText;
    [SerializeField]
    private float doubleMoneyCost;
    public float DoubleMoneyCost
    {
        get
        {
            return doubleMoneyCost;
        }

        set
        {
            doubleMoneyCost = value;
            doubleMoneyText.text = value.ToString();
        }
    }


    public void Start()
    {
        moneyMultiplier = 1;
        missileMultiplier = 1;
        
        //doubleLifeCostText.text = doubleLifeCost.ToString();
        //doubleMissileCostText.text = doubleMissileCost.ToString();
        //doubleShieldText.text = doubleLifeCost.ToString();
        //doubleMissileTimeText.text = doubleMissileCost.ToString();
    }

   

    /*VOLUME CONTROLLER*/

    public Sprite volumeIcon;
    public Sprite volumeMute;

    public GameObject volumeUI;


    public void VolumeHandler(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        AudioManager.Instance.VolumeChange(value);

        volumeUI = GameObject.FindGameObjectWithTag("volume");

        if (value == 0)
        {
            volumeUI.GetComponent<Image>().sprite = volumeMute;
        }
        else
            volumeUI.GetComponent<Image>().sprite = volumeIcon;

    }

  
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OpenMenu()
    {
        //int menuWave = 0;
        //menuScoreText.text = GameManager.Instance.scores.ToString();

        //if (AsteroidSpawner.Instance.nextWave - 1 < 0)
        //    menuWave = 0;
        //else
        //    menuWave = AsteroidSpawner.Instance.nextWave - 1;

        //waveMenuText.text = string.Format("Waves: {0}", menuWave.ToString());

        if (GameManager.Instance.gameOver)
            gameOverText.SetActive(true);
        else if(gameOverText != null)
            gameOverText.SetActive(false);

        Time.timeScale = 0f;
        GameManager.Instance.menu.SetActive(true);
        if(GameManager.Instance.ui != null)
            GameManager.Instance.ui.SetActive(false);

        Slider volumeSlider = GameObject.FindGameObjectWithTag("volumeSlider").GetComponent<Slider>();
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);

        if(SceneManager.GetActiveScene().name != "TITLE")
        {
            GameObject.FindGameObjectWithTag("titleLogo").GetComponent<Image>().sprite = LevelManager.Instance.lvlData.logoPlanet;
            if (PlayerPrefs.GetInt("LevelIndex", 0) > 0)
                GameObject.FindGameObjectWithTag("titleNumber").GetComponent<Text>().text = (PlayerPrefs.GetInt("LevelIndex", 0)+1).ToString();
        }
    }


    public void CloseMenu()
    {
        if (!GameManager.Instance.gameOver)
        {
            Time.timeScale = 1f;
            GameManager.Instance.menu.SetActive(false);
            if (GameManager.Instance.ui != null)
                GameManager.Instance.ui.SetActive(true);
        }

    }


    //Quit
    public void MenuQuit()
    {
        Application.Quit();
    }


    public void PlainScene()
    {
       
       
        StartCoroutine(MenuScreen.Instance.FadeOut("Main"));
    }

    public void TilteScene()
    {
        if (SceneManager.GetActiveScene().name == "CREDITS")
            PlayerPrefs.SetInt("CreditExit", 1);
        else
            PlayerPrefs.SetInt("CreditExit", 0);

        Debug.Log(PlayerPrefs.GetInt("CreditExit", 1));

        SceneManager.LoadScene("TITLE");
    }

    public void CreditsScene()
    {
        if (GameManager.Instance.Money >= GameManager.Instance.moneyGoal)
        {
            int lvlIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            lvlIndex++;

            PlayerPrefs.SetInt("LevelIndex", lvlIndex);

            PlayerPrefs.SetFloat("MoneyGoal", GameManager.Instance.moneyGoal + 50);

            //StartCoroutine(MenuScreen.Instance.FadeOut("Credits"));
            StartCoroutine(MenuScreen.Instance.FadeOut("Credits"));
        }
    }

    public void StartAnim()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("titleCanvas");

        canvas.GetComponent<Animator>().SetTrigger("titleStart");
    }


    public void OpenBrowser(string url)
    {
        Application.OpenURL(url);
    }
    /*************************PANEL FUNCTIONS **************************************/


    //Shield
    public void DoubleShield()
    {
        if (GameManager.Instance.PowerUpEnabled && !GameManager.Instance.shieldUp && !GameManager.Instance.moneyUp)
        {


            GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");

            foreach (GameObject life in lifes)
            {
                var tmpL = life.transform.GetChild(0);
                tmpL.gameObject.SetActive(true);
            }



            GameManager.Instance.shieldUp = true;
            //GameManager.Instance.shieldUI.SetActive(true);

            GameManager.Instance.PowerUpEnabled = false;

            GameManager.Instance.PowerUpImg.SetActive(false);
            GameManager.Instance.PowerUpMissileImg.SetActive(false);
            GameManager.Instance.powerUpPanel.SetActive(false);
            //DoubleShieldCost = Mathf.Round(DoubleShieldCost*= 1.25f);
        }
    }

    //+1 missiles to each planet
    public void DoubleMoney()
    {
        if (GameManager.Instance.PowerUpEnabled && !GameManager.Instance.shieldUp && !GameManager.Instance.moneyUp)
        {

            GameManager.Instance.scoreValue = 20;

            GameManager.Instance.moneyUp = true;

            GameManager.Instance.PowerUpEnabled = false;

            GameManager.Instance.PowerUpImg.SetActive(false);
            GameManager.Instance.PowerUpMissileImg.SetActive(false);
            GameManager.Instance.powerUpPanel.SetActive(false);
            //GameManager.Instance.Money -= DoubleMissileCost;
            //GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            //DoubleMissileCost = Mathf.Round(DoubleMissileCost *= 2f);
        }
    }

    /*************************PANEL FUNCTIONS **************************************/



    ////+1 missiles to each planet
    //public void DoubleMissiles()
    //{
    //    if (GameManager.Instance.PowerUpEnabled && !GameManager.Instance.shieldUp && !GameManager.Instance.missileUp)
    //    {
    //        missileMultiplier++;
    //        missileMultiplierText.gameObject.SetActive(true);
    //        GameManager.Instance.missileLimit += 1;

    //        GameManager.Instance.missileUp = true;

    //        GameManager.Instance.PowerUpEnabled = false;

    //        GameManager.Instance.PowerUpImg.SetActive(false);
    //        GameManager.Instance.PowerUpMissileImg.SetActive(false);

    //        //GameManager.Instance.Money -= DoubleMissileCost;
    //        //GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
    //        //DoubleMissileCost = Mathf.Round(DoubleMissileCost *= 2f);
    //    }
    //}



    //Spread life to next planet instantly
    public void DoubleLife()
    {
        if (GameManager.Instance.Money >= DoubleLifeCost)
        {
            GameManager.Instance.lifeMultiplier = 2;
            GameManager.Instance.lifeTimer = GameManager.Instance.lifeSpreadTime;
            GameManager.Instance.Money -= DoubleLifeCost;
            DoubleLifeCost = Mathf.Round(DoubleLifeCost *= 1.25f);
        }
    }

  


  

   

    //DoubleCooldown
    public void DoubleTime()
    {
        if (GameManager.Instance.Money >= DoubleMissileTime && GameManager.Instance.missileTime != 0.5)
        {
            GameManager.Instance.missileTime = 0.5f;
            GameManager.Instance.timeUI.SetActive(true);
            GameManager.Instance.Money -= DoubleMissileTime;
            //GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            DoubleMissileTime = Mathf.Round(DoubleMissileTime *= 1.25f);
        }
    }



    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("TITLE");
    }



}