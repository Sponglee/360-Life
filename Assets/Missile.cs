using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float turn = 20;

    [SerializeField]
    private float missileLife = 2;


    [SerializeField]
    private Rigidbody homingMissile;

    [SerializeField]
    private float fuseDelay;

    [SerializeField]
    private Transform target;
    private Transform home;

    private bool comingHome = false;
    private float missileRange = 0f;

    private void Start()
    {
        home = transform.parent;
        comingHome = false;
        missileRange = home.GetComponent<Planet>().Range;
        transform.SetParent(AsteroidSpawner.Instance.transform);
        homingMissile = gameObject.GetComponent<Rigidbody>();

        //StartCoroutine(Fire());
    }

    private void Fire()
    {
        //yield return new WaitForSecondsRealtime(fuseDelay);

        float distance = Mathf.Infinity;


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            var diff = (go.transform.parent.position - transform.position).sqrMagnitude;



            if (diff < distance)
            {
                distance = diff;
                if(distance < missileRange)
                {
                    target = go.transform;
                }
                else
                {
                    comingHome = true;
                }

            }
          
        }


    }




    private void FixedUpdate()
    {
        //missileLife -= Time.fixedDeltaTime;

        //if (missileLife < 0)
        //{
        //    missileLife = 2f;
        //    SimplePool.Despawn(gameObject);
        //}

        if (!comingHome)
            Fire();
        else
            target = home;

        if (target == null || homingMissile == null)
        {
            //Destroy(gameObject);
            return;
        }
        //else if (target != firstTarget)
        //{
        //    comingHome = true;
        //}



       

        homingMissile.velocity = transform.forward * speed;

        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {

            comingHome = true;
            home.GetComponent<Planet>().MissileCount--;
            SimplePool.Despawn(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Life"))
        {
            comingHome = false;
           
        }
        else if(other.gameObject.CompareTag("Planet"))
        {
            home.GetComponent<Planet>().MissileCount--;
            SimplePool.Despawn(gameObject);
        }
    }
}