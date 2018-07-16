using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{


    [SerializeField]
    private float range = 18f;
    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    private Stack<Transform> planetTargets;
    [SerializeField]
    private float targetCount = 1;
    public float TargetCount
    {
        get
        {
            targetCount = GameManager.Instance.missileLimit;
            return targetCount;
        }

        set
        {
            targetCount = value;
        }
    }


    private float pewTimer = 2;
    private float missileCoolDown = 2;
    public float MissileCoolDown
    {
        get
        {
            missileCoolDown = GameManager.Instance.missileTime;
            return missileCoolDown;
        }

        set
        {
            missileCoolDown = value;
        }
    }



    private string setTag;
    public string SetTag
    {
        get
        {

            return gameObject.tag;
        }

        set
        {
            setTag = value;
            gameObject.tag = value;

        }
    }



    ////number of simultaneous missiles per planet
    //[SerializeField]
    //private int missileCount=1;
    //public int MissileCount
    //{
    //    get
    //    {
    //        return missileCount;
    //    }

    //    set
    //    {
    //        missileCount = value;
    //    }
    //}





    // Use this for initialization
    void Start()
    {
        planetTargets = new Stack<Transform>();
        targetCount = GameManager.Instance.missileLimit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameObject.CompareTag("Life") && pewTimer <= 0 && planetTargets.Count != 0)
        {

            //StartCoroutine(StopMissiles());
            planetTargets.Clear();
            planetTargets = FindClosestByTag("Debree");
            
            if (planetTargets.Count > 0)
            {
                Debug.Log(planetTargets.Count);
                for (int i = 0; i < TargetCount; i++)
                {
                    if (i < planetTargets.Count)
                    {
                        //Debug.Log("SPAWN " + i + " " + gameObject.name + " " + planetTargets.Count);
                        GameObject tmp = SimplePool.Spawn(GameManager.Instance.missile, gameObject.transform.position, Quaternion.LookRotation(Vector3.forward));
                        tmp.transform.SetParent(gameObject.transform);
                        tmp.GetComponent<Missile>().Target = planetTargets.Pop();
                        pewTimer = MissileCoolDown;
                       
                    }


                }

                //Debug.Log("======");

            }


        }
        else if (gameObject.CompareTag("Life"))
        {
            pewTimer -= Time.fixedDeltaTime;
            planetTargets = FindClosestByTag("Debree");
        }
        else
            pewTimer = missileCoolDown;
    }


    Stack<Transform> FindClosestByTag(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        Stack<Transform> closest = new Stack<Transform>();
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance < Range)
            {
                if (go.transform.parent == null)
                {
                    closest.Push(go.transform);
                    distance = curDistance;
                }
            }
        }
        return closest;
    }


    //private void Fire()
    //{
    //    //planetTargets.Clear();
    //    //if (distance < range /*&& fastestAsteroidSpeed <= goVel.sqrMagnitude*/)
    //    //{
      
    //        //target = go.transform;

    //    //}
    //}



    //private void Fire()
    //{
    //    //yield return new WaitForSecondsRealtime(fuseDelay);
    //    planetTargets.Clear();

    //    float distance = Mathf.Infinity;
    //    float fastestAsteroidSpeed = 0;

    //    //Transform target=null;

    //    //for (int i = 0; i < targetCount; i++)
    //    //{
    //    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Asteroid"))
    //    {
    //        if (planetTargets.Count == 0)
    //        {


    //            Vector3 tmpGoPos = go.transform.TransformPoint(go.transform.position);

    //            var diff = (tmpGoPos - transform.position).sqrMagnitude;
                
    //            //var goRb = go.GetComponent<KeplerOrbitMover>().OrbitData;

    //            //Vector3 goVel = new Vector3((float)goRb.Velocity.x, (float)goRb.Velocity.y, (float)goRb.Velocity.z);
    //            //float angleOfAttack = Vector3.Dot(goVel, go.transform.parent.position - transform.position);


    //            if (diff < distance /*&& angleOfAttack < 0 && goVel.sqrMagnitude > fastestAsteroidSpeed && diff <= range*/)
    //            {

    //                distance = diff;
    //                //fastestAsteroidSpeed = goVel.sqrMagnitude;

    //                if (distance < range /*&& fastestAsteroidSpeed <= goVel.sqrMagnitude*/)
    //                {
    //                    planetTargets.Push(go.transform);
    //                    //target = go.transform;

    //                }

    //            }
    //        }
    //        else
    //        {
    //            Vector3 tmpGoPos = go.transform.TransformPoint(go.transform.position);

    //            var diff = (tmpGoPos - transform.position).sqrMagnitude;
    //            //var goRb = go.GetComponent<KeplerOrbitMover>().OrbitData;

    //            //Vector3 goVel = new Vector3((float)goRb.Velocity.x, (float)goRb.Velocity.y, (float)goRb.Velocity.z);
    //            //float angleOfAttack = Vector3.Dot(goVel, go.transform.parent.position - transform.position);


    //            //if (diff >= distance && diff <= range)
    //            //{

    //            //    //distance = diff;
    //            //    //fastestAsteroidSpeed = goVel.sqrMagnitude;

    //            //    //if (distance < range && fastestAsteroidSpeed <= goVel.sqrMagnitude)
    //            //    //{
    //            planetTargets.Push(go.transform);
    //            //Debug.Log(tmpGoPos + " : " + transform.position);
    //            //target = go.transform;

    //            //}

    //            //}
    //        }


    //    }
    //    //if(target != null)
    //    //    planetTargets.Add(target);
    //    //    yield return new WaitForSecondsRealtime(0.2f);
    //    ////}



    //}








}
