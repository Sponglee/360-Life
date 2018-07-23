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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OpenMenu()
    {
        int menuWave = 0;
        //menuScoreText.text = GameManager.Instance.scores.ToString();

        //if (AsteroidSpawner.Instance.nextWave - 1 < 0)
        //    menuWave = 0;
        //else
        //    menuWave = AsteroidSpawner.Instance.nextWave - 1;

        waveMenuText.text = string.Format("Waves: {0}", menuWave.ToString());

        if (GameManager.Instance.gameOver)
            gameOverText.SetActive(true);
        else
            gameOverText.SetActive(false);

        Time.timeScale = 0f;
        GameManager.Instance.menu.SetActive(true);
        GameManager.Instance.ui.SetActive(false);

    }

    public void CloseMenu()
    {
        if (!GameManager.Instance.gameOver)
        {
            Time.timeScale = 1f;
            GameManager.Instance.menu.SetActive(false);
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

        SceneManager.LoadScene("TITLE");
    }

    public void CreditsScene()
    {
        if (GameManager.Instance.Money >= GameManager.Instance.moneyGoal)
        {
            int lvlIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            lvlIndex++;

            PlayerPrefs.SetInt("LevelIndex", lvlIndex);

            PlayerPrefs.SetFloat("MoneyGoal", GameManager.Instance.moneyGoal + 10);

            //StartCoroutine(MenuScreen.Instance.FadeOut("Credits"));
            StartCoroutine(MenuScreen.Instance.FadeOut("Credits"));
        }
    }


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

    //Spread life to next planet instantly
    public void DoubleMoney()
    {
        if (GameManager.Instance.Money >= DoubleMoneyCost)
        {
            moneyMultiplier++;
            moneyMultiplierText.text = string.Format("x{0}", moneyMultiplier);
            //GameManager.Instance.mps += 10;
            //Debug.Log(GameManager.Instance.mps);
            GameManager.Instance.Money -= DoubleMoneyCost;
            GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            DoubleMoneyCost = Mathf.Round(DoubleMoneyCost *=2f);
        }
    }


    //+1 missiles to each planet
    public void DoubleMissiles()
    {
        if (GameManager.Instance.Money >= DoubleMissileCost)
        {
            missileMultiplier++;
            missileMultiplierText.text = string.Format("x{0}", missileMultiplier);
            GameManager.Instance.missileLimit += 1;
            GameManager.Instance.Money -= DoubleMissileCost;
            GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            DoubleMissileCost = Mathf.Round(DoubleMissileCost *= 2f);
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
            GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            DoubleMissileTime = Mathf.Round(DoubleMissileTime *= 1.25f);
        }
    }


    //Shield
    public void DoubleShield()
    {
        if (GameManager.Instance.Money >= DoubleShieldCost && !GameManager.Instance.shieldUp)
        {


            GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");

            foreach (GameObject life in lifes)
            {
                var tmpL = life.transform.GetChild(0);
                tmpL.gameObject.SetActive(true);
            }



            GameManager.Instance.shieldUp = true;
            //GameManager.Instance.shieldUI.SetActive(true);
            GameManager.Instance.Money -= DoubleShieldCost;
            GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();
            DoubleShieldCost = Mathf.Round(DoubleShieldCost*= 1.25f);
        }
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }


}