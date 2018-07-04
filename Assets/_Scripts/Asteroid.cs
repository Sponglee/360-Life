using UnityEngine;
using System.Collections;
using SimpleKeplerOrbits;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour 
{
	public float tumble;
    public GameObject safespot;
    private float parentCoolDown = 0.5f;

    public float speed=0.3f;

    [SerializeField]
    private int scoreValue;

    void Start ()
	{
        Rigidbody rb = GetComponent<Rigidbody>();

       
        gameObject.GetComponent<KeplerOrbitMover>().AttractorSettings.AttractorObject = AsteroidSpawner.Instance.Sol;
        
        
        Vector3 direction = AsteroidSpawner.Instance.gameObject.transform.position - gameObject.transform.position;

		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = direction * Random.Range(0.05f,speed);

    }

    //public void Update()
    //{
    //    if (gameObject.transform.parent != AsteroidSpawner.Instance.gameObject)
    //    {
    //        parentCoolDown -= Time.deltaTime;
    //        if (parentCoolDown < 0)
    //        {
    //            gameObject.transform.SetParent(AsteroidSpawner.Instance.gameObject.transform);

    //        }
    //    }
    //}



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            SimplePool.Spawn(AsteroidSpawner.Instance.explosion, gameObject.transform.position, Quaternion.identity);

            //Float text spawn, rotate to camera
            GameObject tmp = SimplePool.Spawn(AsteroidSpawner.Instance.fltText, gameObject.transform.position, Quaternion.identity);
            tmp.transform.LookAt(Camera.main.transform);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("+{0}", scoreValue.ToString());

            GameManager.Instance.scores += scoreValue;
            GameManager.Instance.scoreText.text = GameManager.Instance.scores.ToString();

            StartCoroutine(StopDestroy());
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
       
        if (collision.gameObject.CompareTag("Life"))
        {
            //collision.gameObject.SetActive(false);
            SimplePool.Spawn(AsteroidSpawner.Instance.planetExplosion, gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Outline>().enabled = false;

                
            GameManager.Instance.LifeCount--;
            GameManager.Instance.moneyMultiplier.text = string.Format("x{0}", GameManager.Instance.LifeCount);
            collision.gameObject.GetComponent<Outline>().enabled = false;
            collision.gameObject.GetComponent<Planet>().SetTag = "Planet";


            StartCoroutine(StopDestroy());
        }

    }

    private IEnumerator StopDestroy()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
      
    }
}