using SimpleKeplerOrbits;
using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool hasFired = false;
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

    private bool comingHome = false;
    //private float missileRange = 0f;

   
    private void Start()
    {
      
      

        //StartCoroutine(Fire());
    }

    




    private void FixedUpdate()
    {
        if (!hasFired)
        {
            home = transform.parent;
            comingHome = false;
            //missileRange = home.GetComponent<Planet>().Range;
          
            homingMissile = gameObject.GetComponent<Rigidbody>();
           
            hasFired = true;
        }

        missileCoolDown -= Time.fixedDeltaTime;

        if (missileCoolDown < 0 && Target == null)
        {
            missileCoolDown = missileLife;
            SimplePool.Despawn(gameObject);
            //home.GetComponent<Planet>().MissileCount--;
        }

        //if (!comingHome)

        //else
        //    target = home;
        if (Target == null || homingMissile == null)
        {
            if (hasFired)
            {
                //Debug.Log("HASFIRED");
                hasFired = false;
                SimplePool.Despawn(gameObject);
            }
            //Destroy(gameObject);
            return;
        }
        else if (Target != null)
        {
            hasFired = true;
        }
        //else if (target != firstTarget)
        //{
        //    comingHome = true;
        //}



       

        homingMissile.velocity = transform.forward * speed;

        var targetRotation = Quaternion.LookRotation(Target.position - transform.position);

        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {

            comingHome = true;
            missileLife = 2f;
            hasFired = false;
            SimplePool.Despawn(gameObject);
            Destroy(other.gameObject);
            //home.GetComponent<Planet>().MissileCount--;
        }
        //else if (other.gameObject.CompareTag("Life") && comingHome)
        //{
        //    comingHome = false;
        //    SimplePool.Despawn(gameObject);
           
        //}
        else if(other.gameObject.CompareTag("Planet"))
        {
            hasFired = false;
            missileLife = 2f;
            SimplePool.Despawn(gameObject);
            //home.GetComponent<Planet>().MissileCount--;
        }
    }
}