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


	// Use this for initialization
	void Awake() {

        Debug.Log("REEEEEEEEEEEEE");

        //if (firstLaunch)
        //{
        //    firstLaunch = false;
        //    RandomiseStuff();
        //}

        Debug.Log(levelIndex + " : " + currentLevel);
        //DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().name == "TITLE")
        {
            levelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            if (levelIndex > 0)
            {
                levelNumberText.gameObject.SetActive(true);
                levelNumberText.text = (levelIndex + 1).ToString();
            }

            Debug.Log(levelIndex + " : " + currentLevel);
            Debug.Log(PlayerPrefs.GetString("LevelInfo", "0,0,0,0"));

            if (levelIndex != 0 && currentLevel != levelIndex)
                RandomiseStuff();
        }


        LoadStuff(PlayerPrefs.GetString("LevelInfo", "0,0,0,0"));
    }
	
	



    public void RandomiseStuff()
    {
        lvlInfo.earthMat = Random.Range(0, earthMats.Length);
        lvlInfo.backGround = Random.Range(0, backGrounds.Length);
        lvlInfo.starColor = Random.Range(0, starColors.Length);
        lvlInfo.hazardMat = Random.Range(0, hazardMats.Length);
        lvlInfo.earthIndex = Random.Range(0, 3);

        string saveString = lvlInfo.earthMat.ToString() + "," + lvlInfo.backGround.ToString() + "," 
            + lvlInfo.starColor.ToString() + "," + lvlInfo.hazardMat.ToString() /*+ "," + lvlInfo.starColor.ToString()*/;

        PlayerPrefs.SetString("LevelInfo", saveString);

        //Earth position per lvl
        PlayerPrefs.SetInt("EarthIndex", Random.Range(0, 3));
        //Randomise other planet colors
        PlayerPrefs.SetInt("VenusColor", Random.Range(0, planetColors.Length));
        PlayerPrefs.SetInt("MarsColor", Random.Range(0, planetColors.Length));
    }

    public void LoadStuff(string info)
    {
        string[] tokens = info.Split(',');

        lvlData.earthMat = earthMats[int.Parse(tokens[0])];
        lvlData.backGround = backGrounds[int.Parse(tokens[1])];
        lvlData.starColor = starColors[int.Parse(tokens[2])];
        lvlData.hazardMat = hazardMats[int.Parse(tokens[3])];
        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
    }

}
