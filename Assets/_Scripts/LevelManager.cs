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

    public string[] planetNamesPrefs;

    public string[] actorNamesPrefs;

    public string[] subtitlePrefabs;
    /*******STRUCT PREFS **********/

    //FOR TITLE SCREEN NAME
    public Text levelNumberText;
    public Text levelSubtitleText;


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
        public int subIndex;
    }

    public LevelData lvlData;
    public struct LevelData
    {
        public Sprite backGround;
        //public Color backColor;
        public Color starColor;
        public Material earthMat;
        public Color[] planetColor;
        public Material hazardMat;
        public Color32 hazardCol;
        //public Color shipColor;
        public Sprite logoPlanet;
        public string subtitle;
        
    }



    #region subtitles

    public string subtitlestring = ",The Phantom Menace,Back in the Habit,The Beginning,Die Harder,Vendetta,Golden Asteroid," +
        "The Squeakquel,Chipwrecked,Cruise Control,First Blood Part II,On the Move,Armed and Fabulous,Citizens on Patrol," +
        "Back in Training,The Revenge,Back to Perfection,Full Throttle,Back in Business,Heading Home,The New Batch,The Last Stand,State of the Union,The Next Chapter,The Final Chapter," +
        "Resurrection,Risk Addiction,The Quickening,I Want To Believe,Re-Entry,Beyond Cyberspace,Dark Territory,The Cradle Of Life,The Quest For Peace,With a Vengeance," +
        "Attack of the Clones,Dawn of the Dinosaurs,Dawn of Justice,The Sequel,First Contact,28 Weeks Later,Beyond,Days Of Future Past,The Force Awakens,The Wrath Of Khan," +
        "The Empire Strikes Back,Fury Road,Judgment Day";


    public string planetNames = "Kasama,Theseus 5,Monarch 5,Ariana Prime,Seuden 9,Lakota 5,Avanyu,Taizu,Pollyanna,Vluub,Giana Prime,Knara Prime,Azrail,Dosi,Zodiac," +
        "Kaladria,Abellio,Eta Algira,Shadowfax,Pohl,Limbo,Sigma Corinthi,Araimis,Ch'Koris, Seuden 5,Banton Prime,Narhadul,Aporia,Sirona,Suza 5,Delta Atheni," +
        "Nabia Prime,Abassi,Zariah, New Earth,Bellataine Prime,Churchill,Gilgamesh,Eden,Xanthondo,Mete,Zoogee,Telenet,Nicotte 6,Kosmikox-wing," +
        "Protogee,Runomics -x,Optimonyes,Memmibolt,Brutenico,Thametro,Mortatronium,Geonance -x,Radisonic 10,Zeronaut,Zephelake 3,Systemaphile,Calypho VI,Outernakko";
    #endregion


    public string actorNames = "Robert Downey Jr.,Tom Cruise,Matt Damon,Bradley Cooper,Jennifer Lawrence,Kristen Stewart,Johnny Depp," +
        "Leonardo DiCaprio,Christian Bale,Mark Wahlberg,Keanu Reeves,Denzel Washington,Will Smith,Hugh Jackman,Matthew McConnaughney,Tom Hanks,George Clooney,Daniel Craig,Ben Stiller," +
        "Ben Affleck,Vin Diesel,Chris Pratt,Liam Neeson,Angelina Jolie,Michael Fassbender,Russell Crowe,Ryan Gosling,Ben Affleck,Margot Robbie,Emma Stone,Natalie Portman,Tom Hanks,"
        + "Denzel Washington,Mark Wahlberg,Matt Damon,Samuel L.Jackson,Johnny Depp,Christian Bale,Matthew McConaughey,Morgan Freeman,Jake Gyllenhaal,Jeremy Renner,Dwayne Johnson";
    /********STRUCT PREFS **********/



    public int currentLevel;
    public int levelIndex;
    public bool firstLaunch = true;

    void Start()
    {
        planetNamesPrefs = planetNames.Split(',');
        actorNamesPrefs = actorNames.Split(',');
        subtitlePrefabs = subtitlestring.Split(',');
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
                levelSubtitleText = GameObject.FindGameObjectWithTag("subTitle").GetComponent<Text>();
                levelSubtitleText.text = lvlData.subtitle;
            }

            //Debug.Log(levelIndex + " : " + currentLevel);
            Debug.Log(PlayerPrefs.GetString("LevelInfo", "0,0,0,0,0,0"));

            if (levelIndex != 0 && currentLevel != levelIndex)
                RandomiseStuff();
        }

        /*earthMaterial, backGround, starColor, hazardsMaterial, logoPlanet*/
        LoadStuff(PlayerPrefs.GetString("LevelInfo", "0,0,0,0,0,0"));

        //Set subtitle name
    
    }

  
	



    public void RandomiseStuff()
    {
        lvlInfo.earthMat = SmartRandomizer(earthMats.Length, lvlInfo.earthMat);
        lvlInfo.backGround = SmartRandomizer(backGrounds.Length, lvlInfo.backGround);
        //Randomize if background is grey
        if (lvlInfo.backGround == 3)
        {
            PlayerPrefs.SetInt("BackColor", SmartRandomizer(backColors.Length, PlayerPrefs.GetInt("BackColor", 0)));
        }
        lvlInfo.starColor = SmartRandomizer(starColors.Length, lvlInfo.starColor);
        lvlInfo.hazardMat = SmartRandomizer(hazardMats.Length, lvlInfo.hazardMat);
        lvlInfo.earthIndex = SmartRandomizer(3, lvlInfo.earthIndex);
        lvlInfo.logoPlanet = SmartRandomizer(logoPlanets.Length, lvlInfo.logoPlanet);
        lvlInfo.subIndex = SmartRandomizer(subtitlePrefabs.Length, lvlInfo.subIndex);

        //Generate a save string
        string saveString = lvlInfo.earthMat.ToString() + "," + lvlInfo.backGround.ToString() + "," 
            + lvlInfo.starColor.ToString() + "," + lvlInfo.hazardMat.ToString() + "," + lvlInfo.logoPlanet.ToString() + "," + lvlInfo.subIndex.ToString()/*+ "," + lvlInfo.starColor.ToString()*/;

        PlayerPrefs.SetString("LevelInfo", saveString);

        //planetName
        PlayerPrefs.SetString("PlanetName", planetNamesPrefs[Random.Range(0, planetNamesPrefs.Length)]);
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
        lvlData.subtitle = subtitlePrefabs[int.Parse(tokens[5])];
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
