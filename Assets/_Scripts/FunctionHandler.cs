using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHandler : Singleton<FunctionHandler> {


    public GameObject gameOverText;
    public Text menuScoreText;



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
}
