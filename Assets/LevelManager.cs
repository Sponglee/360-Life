using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>{


    public GameObject[] planetPrefs;

    public Sprite[] backGrounds;

    public Color[] backColors;

    public Color[] starColors;

    public Material[] earthMats;

    public Color[] planetColors;

    public Material[] hazardMats;
    
    public Color[] shipColors;

    public GameObject[] hazardPrefs;


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

    }

    public int currentLevel;
    public int levelIndex;

	// Use this for initialization
	void Awake   () {

        //DontDestroyOnLoad(gameObject);

        levelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        Debug.Log(levelIndex + " : " + currentLevel);
        Debug.Log(PlayerPrefs.GetString("LevelInfo", "0,0,0"));

        if (SceneManager.GetActiveScene().name == "Main" && (currentLevel != levelIndex || currentLevel == 0))
        {
            RandomiseStuff();
        }


        LoadStuff(PlayerPrefs.GetString("LevelInfo", "0,0,0"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}




    public void RandomiseStuff()
    {
        lvlInfo.earthMat = Random.Range(0, earthMats.Length);
        lvlInfo.backGround = Random.Range(0, backGrounds.Length);
        lvlInfo.starColor = Random.Range(0, starColors.Length);


        string saveString = lvlInfo.earthMat.ToString() + "," + lvlInfo.backGround.ToString() + "," + lvlInfo.starColor.ToString();

        PlayerPrefs.SetString("LevelInfo", saveString);
    }

    public void LoadStuff(string info)
    {
        string[] tokens = info.Split(',');

        lvlData.earthMat = earthMats[int.Parse(tokens[0])];
        lvlData.backGround = backGrounds[int.Parse(tokens[1])];
        lvlData.starColor = starColors[int.Parse(tokens[2])];

        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
    }

}
