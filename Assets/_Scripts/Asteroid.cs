using UnityEngine;
using System.Collections;
using SimpleKeplerOrbits;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour 
{
	public float tumble;
    public GameObject debreePref;
    private float parentCoolDown = 0.5f;

    public float speed=2f;



    private bool collisionInProgress = false;
    private bool start = true;

    void Start ()
	{
        Rigidbody rb = GetComponent<Rigidbody>();


        gameObject.GetComponent<KeplerOrbitMover>().AttractorSettings.AttractorObject = AsteroidSpawner.Instance.Sol;


        Vector3 direction = AsteroidSpawner.Instance.gameObject.transform.position - gameObject.transform.position;

		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = direction * Random.Range(1f,speed);

    }

    //public void Update()
    //{
    //    if(start)
    //    {

    //    }
    //}



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Life"))
        {

            //collision.gameObject.SetActive(false);
            SimplePool.Spawn(AsteroidSpawner.Instance.planetExplosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(debreePref, transform.position, Quaternion.identity);

            if (!GameManager.Instance.shieldUp)
            {
                collision.gameObject.GetComponent<Outline>().enabled = false;
                //GameManager.Instance.lifePlanets.Enqueue(collision.gameObject);
                Debug.Log("LIFE-- " + collision.gameObject.name);


                collision.gameObject.GetComponent<Outline>().enabled = false;
                collision.gameObject.GetComponent<Planet>().SetTag = "Planet";
                GameManager.Instance.LifeCount--;
                //GameManager.Instance.scoreMultip.text = string.Format("x{0}", GameManager.Instance.LifeCount);


            }
            else
            {
               

                GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");

                foreach (GameObject life in lifes)
                {
                    life.transform.GetChild(0).gameObject.SetActive(false);
                }
                GameManager.Instance.shieldUp = false;
            }

            StartCoroutine(StopDestroy());
        }
        else if (!collision.gameObject.CompareTag("Missile") && !collision.gameObject.CompareTag("Debree") && !collisionInProgress)
        {
            collisionInProgress = true;

            SimplePool.Spawn(AsteroidSpawner.Instance.explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(debreePref, transform.position, Quaternion.identity);


            StartCoroutine(StopDestroy());
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);








        }



        //if (collision.gameObject.CompareTag("Planet") && !collisionInProgress)
        //{
        //    SimplePool.Spawn(AsteroidSpawner.Instance.explosion, gameObject.transform.position, Quaternion.identity);
        //    StartCoroutine(StopDestroy());
        //}


    }

        private IEnumerator StopDestroy()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        collisionInProgress = false;
        Destroy(gameObject);
      
    }
  
}