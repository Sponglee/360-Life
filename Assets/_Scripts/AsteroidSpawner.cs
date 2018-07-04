using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : Singleton<AsteroidSpawner> {
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private GameObject randomHazard;


    public GameObject fltText;

    public Transform Sol;


    public GameObject explosion;
    public GameObject planetExplosion;


    public GameObject asteroids;

    float ang = 0f;

    public void Start()
    {

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            //if (gameOver)
            //{
            //    restartText.text = "Press 'R' for Restart";
            //    restart = true;
            //    break;
            //}
            for (int i = 0; i <= hazardCount; i++)
            {

                    Vector3 center = transform.position;

                    Vector3d spawnPosition = RandomCircle(center,Random.Range(1f,30f));

                    Quaternion spawnRotation = Quaternion.identity;






                int rng = Random.Range(0, 3);
                randomHazard = hazards[rng];
                GameObject tmp = Instantiate(randomHazard,gameObject.transform);

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


    Vector3d RandomCircle(Vector3 center, float radius)
    {
        float randAngle = Random.Range(00f,180f);

        //ang += randAngle%360;
        Vector3d pos;
        pos.x = center.x + radius * Mathf.Sin(randAngle * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Sin(randAngle * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
