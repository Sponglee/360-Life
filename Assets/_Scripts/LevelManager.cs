using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>{

    /*******STRUCT PREFS **********/
    public GameObject[] planetPrefs;

    public Sprite[] backGrounds;

    public Color[] backColors;

    public Color[] starColors;

    public Material[] earthMats;

    public Color[] planetColors;

    public Material[] hazardMats;

    public Color[] hazardColors;

    public Color[] shipColors;

    public GameObject[] hazardPrefs;

    public Sprite[] logoPlanets;

    
    /*******STRUCT PREFS **********/


    public Text levelNumberText;

    LevelInfo lvlInfo;
    public struct LevelInfo 
    {
        public int backGround;
        public int backColor;
        public int starColor;
        public int earthMat;
        public int[] planetColor;
        public int hazardMat;
        public int shipColor;
        public int logoPlanet;
        public int earthIndex;
    }

    public LevelData lvlData;
    public struct LevelData
    {
        public Sprite backGround;
        public Color backColor;
        public Color starColor;
        public Material earthMat;
        public Color[] planetColor;
        public Material hazardMat;
        public Color32 hazardCol;
        public Color shipColor;
        public Sprite logoPlanet;

    }

    public int currentLevel;
    public int levelIndex;
    public bool firstLaunch = true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {

        //if (firstLaunch)
        //{
        //    firstLaunch = false;
        //    RandomiseStuff();
        //}

        //Debug.Log(levelIndex + " : " + currentLevel);
        //DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().name == "TITLE")
        {
            levelIndex = PlayerPrefs.GetInt("LevelIndex", 0);

            //Enable tutorial
            if (levelIndex == 0)
                PlayerPrefs.SetInt("TutorialTrigger", 0);

            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            if (levelIndex > 0)
            {
                levelNumberText = GameObject.FindGameObjectWithTag("titleNumber").GetComponent<Text>();
                levelNumberText.text = (levelIndex + 1).ToString();
            }

            //Debug.Log(levelIndex + " : " + currentLevel);
            //Debug.Log(PlayerPrefs.GetString("LevelInfo", "0,0,0,0,0"));

            if (levelIndex != 0 && currentLevel != levelIndex)
                RandomiseStuff();
        }

        /*earthMaterial, backGround, starColor, hazardsMaterial, logoPlanet*/
        LoadStuff(PlayerPrefs.GetString("LevelInfo", "0,0,0,0,0"));
    }

  
	



    public void RandomiseStuff()
    {
        lvlInfo.earthMat = SmartRandomizer(earthMats.Length, lvlInfo.earthMat);
        lvlInfo.backGround = SmartRandomizer(backGrounds.Length, lvlInfo.backGround);
        lvlInfo.starColor = SmartRandomizer(starColors.Length, lvlInfo.starColor);
        lvlInfo.hazardMat = SmartRandomizer(hazardMats.Length, lvlInfo.hazardMat);
        lvlInfo.earthIndex = SmartRandomizer(3, lvlInfo.earthIndex);
        lvlInfo.logoPlanet = SmartRandomizer(logoPlanets.Length, lvlInfo.logoPlanet);

        string saveString = lvlInfo.earthMat.ToString() + "," + lvlInfo.backGround.ToString() + "," 
            + lvlInfo.starColor.ToString() + "," + lvlInfo.hazardMat.ToString() + "," + lvlInfo.logoPlanet.ToString()/*+ "," + lvlInfo.starColor.ToString()*/;

        PlayerPrefs.SetString("LevelInfo", saveString);

        //Earth position per lvl
        PlayerPrefs.SetInt("EarthIndex", Random.Range(0, 3));
        //Randomise other planet colors
        PlayerPrefs.SetInt("VenusColor", SmartRandomizer(planetColors.Length, PlayerPrefs.GetInt("VenusColor",0)));
        PlayerPrefs.SetInt("MarsColor", SmartRandomizer(planetColors.Length, PlayerPrefs.GetInt("MarsColor", 0)));
    }

    public void LoadStuff(string info)
    {
        string[] tokens = info.Split(',');

        lvlData.earthMat = earthMats[int.Parse(tokens[0])];
        lvlData.backGround = backGrounds[int.Parse(tokens[1])];
        lvlData.starColor = starColors[int.Parse(tokens[2])];
        lvlData.hazardMat = hazardMats[int.Parse(tokens[3])];
        lvlData.hazardCol = hazardColors[int.Parse(tokens[3])];
        lvlData.logoPlanet = logoPlanets[int.Parse(tokens[4])];
        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
    }


    public int SmartRandomizer(int length, int lastValue)
    {
        int result = 0;
        do
        {
            result = Random.Range(0, length);

        }
        while (result == lastValue);

        return result;
    }
}
