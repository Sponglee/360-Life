using UnityEngine;
using System.Collections;
using SimpleKeplerOrbits;

public class Asteroid : MonoBehaviour 
{
	public float tumble;
    public GameObject safespot;
    private float parentCoolDown = 0.5f;

    public float speed=0.3f;


    void Start ()
	{
        Rigidbody rb = GetComponent<Rigidbody>();

       
        gameObject.GetComponent<KeplerOrbitMover>().AttractorSettings.AttractorObject = AsteroidSpawner.Instance.Sol;
        
        
        Vector3 direction = AsteroidSpawner.Instance.gameObject.transform.position - gameObject.transform.position;

		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = direction * speed;

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
            Instantiate(AsteroidSpawner.Instance.explosion, gameObject.transform);
            StartCoroutine(StopDestroy());
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
       
        if (collision.gameObject.CompareTag("Life"))
        {
            collision.gameObject.SetActive(false);
            Instantiate(AsteroidSpawner.Instance.planetExplosion, gameObject.transform);
            StartCoroutine(StopDestroy());
        }

    }

    private IEnumerator StopDestroy()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
      
    }
}