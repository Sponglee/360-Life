using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidSpawner : Singleton<AsteroidSpawner>
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public float waveMaxTime;
    public float waveTimer = 0;
    public Slider lifeSlider;
    public Text waveCountText;
    public int nextWave = 0;
    private GameObject randomHazard;


    public GameObject fltText;

    public Transform Sol;


    public GameObject explosion;
    public GameObject planetExplosion;


    public GameObject asteroids;

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
        gameObject.transform.Rotate(Vector3.up, Random.Range(10f, 45f));
        while (true)
        {
            nextWave++;
            waveCountText.text = nextWave.ToString();

            waveTimer = 0;

            waveMaxTime = hazardCount * spawnWait + waveWait;
            //if (gameOver)
            //{
            //    restartText.text = "Press 'R' for Restart";
            //    restart = true;
            //    break;
            //}





            for (int i = 0; i <= hazardCount; i++)
            {




                Vector3 center = transform.position;

                Vector3 spawnPosition = RandomCircle(center, Random.Range(10f, 12f));

                Quaternion spawnRotation = Quaternion.identity;






                int rng = Random.Range(0, 3);
                randomHazard = hazards[rng];
                GameObject tmp = Instantiate(randomHazard, /*spawnPosition, Quaternion.identity,*/ gameObject.transform);

                //tmp.GetComponent<KeplerOrbitMover>().OrbitData.Position = spawnPosition;

                //tmp.GetComponent<KeplerOrbitMover>().SetAutoCircleOrbit();


                yield return new WaitForSeconds(spawnWait);

            }


            //if (gameOver)
            //{
            //    restartText.text = "Press 'R' for Restart";
            //    restart = true;
            //    break;
            //}



            hazardCount += 3;
         
            yield return new WaitForSeconds(waveWait);

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