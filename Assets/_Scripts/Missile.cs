using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool started = false;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float turn = 20;

    [SerializeField]
    private float missileLife = 0.5f;
    private float missileCoolDown;

    [SerializeField]
    private Rigidbody homingMissile;

    [SerializeField]
    private float fuseDelay;

   

    [SerializeField]
    private Transform target;
    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    private Transform home;

    [SerializeField]
    private bool comingHome = false;
    //private float missileRange = 0f;

   
    private void Start()
    {
      
      

        //StartCoroutine(Fire());
    }

    




    private void FixedUpdate()
    {
        if (!started)
        {
            home = transform.parent;
            comingHome = false;
            //missileRange = home.GetComponent<Planet>().Range;
            transform.SetParent(AsteroidSpawner.Instance.asteroidHolder);
            homingMissile = gameObject.GetComponent<Rigidbody>();
           
            started = true;
        }

        missileCoolDown -= Time.fixedDeltaTime;

        if (missileCoolDown < 0 && Target == null)
        {
            missileCoolDown = missileLife;
            //SimplePool.Despawn(gameObject);
            //home.GetComponent<Planet>().MissileCount--;
        }

        //if (comingHome)
        //    target = home;

        if (Target == null || homingMissile == null || Target.CompareTag("Planet") || !Target.gameObject.activeSelf)
        {
            if (started)
            {
                //Debug.Log("HASFIRED");
                started = false;
                SimplePool.Despawn(gameObject);
            }
            //Destroy(gameObject);
            return;
        }
        //else if (Target != null || Target.gameObject.activeSelf)
        //{
        //    started = true;
        //}
        //else if (target != firstTarget)
        //{
        //    comingHome = true;
        //}



        //transform.position = Vector3.MoveTowards(transform.position, Target.position, 0.1f);

        homingMissile.velocity = transform.forward * speed;

        Vector3 diff;

        diff.x = Target.position.x - transform.position.x;
        diff.y = Target.position.y - transform.position.y;
        diff.z = Target.position.z - transform.position.z;

        var targetRotation = Quaternion.LookRotation(diff);

        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }



    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Debree") || other.gameObject.CompareTag("PowerUp"))
        {
            //Debug.Log("HHHHHHHHHHHOOOOOOOOOOOOOOOOOMMMMMMMMMMMMMMMMEEEEEEEEEEEEEE");
            comingHome = true;
            //missileLife = 2f;
            //other.gameObject.GetComponent<Planet>().PlanetTargets.Clear();
            target = home;
           
            //gameObject.transform.SetParent(AsteroidSpawner.Instance.solarSystem);
            ////SimplePool.Despawn(gameObject);
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Life") && comingHome)
        {
            //Debug.Log("LLLLLLLLLLLLLIIIIIIIIIIIIIIIIIIIIFFFFFFFFFFFFFEEEEEEEEE");
            comingHome = false;
            started = false;


            SimplePool.Despawn(gameObject);

            //Destroy(gameObject);


            //if(transform.Find("Debree(Clone)") != null)
            //{
            //    Destroy(transform.Find("Debree(Clone)").gameObject);
            //}
        }
        //else if (other.gameObject.CompareTag("Planet"))
        //{
        //    hasFired = false;
        //    missileLife = 2f;
        //    SimplePool.Despawn(gameObject);
        //    //home.GetComponent<Planet>().MissileCount--;
        //}
    }
}