﻿using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidSpawner : Singleton<AsteroidSpawner>
{
    public GameObject[] hazards;
    public Transform[] asteroidSpots;

    public Vector3 spawnValues;
    public int waveCount = 0;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public float waveMaxTime;
    public float waveTimer = 0;
    public Slider lifeSlider;
    public Text waveCountText;
    public int nextWave = 0;
    private GameObject randomHazard;



    public Transform Sol;

    public Transform fltTextHolder;
    public Transform explosionHolder;
    public Transform asteroidHolder;
    public GameObject fltText;
    public GameObject explosion;
    public GameObject planetExplosion;
    public Transform solarSystem;

    //public GameObject asteroids;

    float ang = 0f;




    public void Start()
    {
        nextWave = 0;
        waveMaxTime = 0;
        waveCountText.text = nextWave.ToString();

        StartCoroutine(SpawnWaves());
    }



    private void Update()
    {
        waveTimer += Time.fixedDeltaTime;
        lifeSlider.value = waveTimer / waveMaxTime;
    }
    IEnumerator SpawnWaves()
    {

        yield return new WaitForSeconds(startWait);
        
        while (true)
        {
            nextWave++;
            waveCountText.text = nextWave.ToString();
            waveCount++;
            waveTimer = 0;

            waveMaxTime = waveWait;
            //if (gameOver)
            //{
            //    restartText.text = "Press 'R' for Restart";
            //    restart = true;
            //    break;
            //}





            //for (int i = 0; i <= hazardCount; i++)
            //{
                Vector3 center = transform.position;

                Vector3 spawnPosition = asteroidSpots[Random.Range(0, 4)].position;

                Quaternion spawnRotation = Quaternion.identity;

                int rng = Random.Range(0, 3);
                randomHazard = hazards[rng];
                GameObject tmp = Instantiate(randomHazard, spawnPosition, Quaternion.identity, asteroidHolder);

                //tmp.GetComponent<KeplerOrbitMover>().OrbitData.Position = spawnPosition;

                //tmp.GetComponent<KeplerOrbitMover>().SetAutoCircleOrbit();


            //    yield return new WaitForSeconds(spawnWait);

            //}


            //if (gameOver)
            //{
            //    restartText.text = "Press 'R' for Restart";
            //    restart = true;
            //    break;
            //}


         
            //hazardCount += 1;
            
            yield return new WaitForSeconds(waveWait);
            if(waveCount<=10)
            {
                waveWait -= Random.Range(0.05f,0.05f);
               
            }

            if (waveCount== Random.Range(3,5))
            {
                gameObject.transform.Rotate(Vector3.up, Random.Range(15f, 30f));
            }
            else if (waveCount > Random.Range(9, 21))
            {
                waveWait = 0.5f;
                //waveCount = 0;
            }   
            else if (waveCount>20)
            {
                waveWait = 2f;
                waveCount = 0;
            }
            if (GameManager.Instance.missileTime != 2)
            {
                GameManager.Instance.missileTime = 2;
                GameManager.Instance.timeUI.SetActive(false);
            }
        }
    }


    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float randAngle = Random.Range(00f, 360f);

        //ang += randAngle%360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(randAngle * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Sin(randAngle * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}