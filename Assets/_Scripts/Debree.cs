using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debree : MonoBehaviour
{


    [SerializeField]
    private bool homing = false;
    [SerializeField]
    private Transform homingTrans;

    [SerializeField]
    private float speed  = 0.05f;

    [SerializeField]
    private int scoreValue=10;
    public int ScoreValue
    {
        get
        {
            scoreValue = GameManager.Instance.scoreValue;
            return scoreValue;
        }

        set
        {
            scoreValue = value;
        }
    }


    private Rigidbody rb;

    //PowerUp check
    private bool isPowerUp = false;
    public bool IsPowerUp
    {
        get
        {
            return isPowerUp;
        }

        set
        {
            isPowerUp = value;
        }
    }

  
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length>1)
            Destroy(gameObject);
        if (isPowerUp)
        {
            gameObject.tag = "PowerUp";
        }
        else
            gameObject.tag = "Debree";

        rb = GetComponent<Rigidbody>();

        Vector3 direction = gameObject.transform.position - AsteroidSpawner.Instance.gameObject.transform.position;

        
        rb.velocity = direction * Random.Range(0.01f, speed);

    }




    private void Update()
    {
        if(homing && homingTrans != null)
        {
            transform.position = homingTrans.position;
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Missile") && !homing)
        {
            homing = true;
            homingTrans = other.transform;
           
            //SimplePool.Despawn(gameObject);
            //gameObject.transform.SetParent(other.gameObject.transform);

            //gameObject.tag = "DebreeHome";
        }
        if (other.gameObject.CompareTag("Life") && homing)
        {
            if(isPowerUp)
            {
                    //Enable powerUps
                    if(!GameManager.Instance.PowerUpEnabled && !GameManager.Instance.shieldUp && !GameManager.Instance.moneyUp)
                    {
                        GameManager.Instance.PowerUpEnabled = true;
                        GameManager.Instance.PowerUpImg.SetActive(true);
                        GameManager.Instance.PowerUpMissileImg.SetActive(true);
                        GameManager.Instance.powerUpPanel.SetActive(true);
                    }
                  
            }
            else
            {
                //Float text spawn, rotate to camera
                GameObject tmp = SimplePool.Spawn(AsteroidSpawner.Instance.fltText, gameObject.transform.position, Quaternion.identity);
                tmp.transform.LookAt(Camera.main.transform);
                tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("+{0}$", ScoreValue.ToString());

                //Add money
                GameManager.Instance.Money += ScoreValue;
                //GameManager.Instance.moneyText.text = GameManager.Instance.Money.ToString();


               
            }
            homing = false;
            homingTrans = null;
            Destroy(gameObject);
        }
    }

}
