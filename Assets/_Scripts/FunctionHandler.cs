using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHandler : Singleton<FunctionHandler> {


    public GameObject gameOverText;
    public Text menuScoreText;




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


    public void Start()
    {
        doubleLifeCostText.text = doubleLifeCost.ToString();
        doubleMissileCostText.text = doubleMissileCost.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void OpenMenu()
    {
        menuScoreText.text = GameManager.Instance.scores.ToString();

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
        SceneManager.LoadScene("Main");

    }

    public void TiltScene()
    {
        SceneManager.LoadScene("Tilted");
    }

    public void DoubleLife()
    {
        if(GameManager.Instance.money >= DoubleLifeCost)
        {
            GameManager.Instance.lifeMultiplier = 2;
<<<<<<< HEAD

            GameManager.Instance.lifeTimer = GameManager.Instance.lifeSpreadTime;


            GameManager.Instance.lifeSpreadTime = 1;

=======
            GameManager.Instance.lifeSpreadTime = 1;
>>>>>>> parent of e588012... 06.07.18
            GameManager.Instance.money -= DoubleLifeCost;
            DoubleLifeCost *=2f;
        }
    }


    public void DoubleMissiles()
    {
        if (GameManager.Instance.money >= DoubleMissileCost)
        {
            GameManager.Instance.missileLimit += 1;
            GameManager.Instance.money -= DoubleMissileCost;
            DoubleMissileCost *= 2f;
        }
    }

    public void DoubleMoney()
    {
        if (GameManager.Instance.money >= DoubleLifeCost)
        {
            GameManager.Instance.lifeMultiplier = 2;
            GameManager.Instance.lifeSpreadTime = 1;
            GameManager.Instance.money -= DoubleLifeCost;
            DoubleLifeCost *= 2f;
        }
    }

}
