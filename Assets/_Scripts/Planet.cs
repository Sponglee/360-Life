using SimpleKeplerOrbits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {


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
    void Start () {
        planetTargets = new Stack<Transform>();
        targetCount = GameManager.Instance.missileLimit;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if (gameObject.CompareTag("Life") && pewTimer <= 0 && planetTargets.Count != 0)
        {

            //StartCoroutine(StopMissiles());
          
            if(planetTargets.Count>0)
            {
                for (int i = 0; i < TargetCount; i++)
                {
                    if(i<planetTargets.Count)
                    {
                        Debug.Log("SPAWN " + i + " " + gameObject.name + " " + planetTargets.Count);
                        GameObject tmp = SimplePool.Spawn(GameManager.Instance.missile, gameObject.transform.position, Quaternion.identity);
                        tmp.GetComponent<Missile>().Target = planetTargets.Pop();
                        pewTimer = targetCount;
                        tmp.transform.SetParent(AsteroidSpawner.Instance.transform);
                    }
                  

                }

                planetTargets.Clear();

            }


        }
        else if (gameObject.CompareTag("Life"))
        {
            pewTimer -= Time.fixedDeltaTime;
            Fire();
        }
        else
            pewTimer = missileCoolDown;
	}


    //private IEnumerator StopMissiles()
    //{
    //    foreach (Transform pT in planetTargets)
    //    {
    //        GameObject tmp = SimplePool.Spawn(GameManager.Instance.missile, gameObject.transform.position, Quaternion.identity);
    //        tmp.GetComponent<Missile>().Target = pT;
    //        pewTimer = targetCount;
    //        tmp.transform.SetParent(AsteroidSpawner.Instance.transform);
    //        yield return new WaitForSecondsRealtime(0.1f);
    //    }
    //}
    //private bool PrepeareFire()
    //{
    //    //yield return new WaitForSecondsRealtime(fuseDelay);

     


    //    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Asteroid"))
    //    {
    //        float diff = (go.transform.parent.position - this.transform.position).sqrMagnitude;

    //       // Debug.Log(diff + " : " + distance);

    //        if (diff < range)
    //        {
    //            planetTarget = go.transform;
    //            return true;

    //        }
    //    }

    //    return false;
    //}

    private void Fire()
    {
        //yield return new WaitForSecondsRealtime(fuseDelay);
        planetTargets.Clear();

        float distance = Mathf.Infinity;
        float fastestAsteroidSpeed = 0;
      
        //Transform target=null;

        //for (int i = 0; i < targetCount; i++)
        //{
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if(planetTargets.Count == 0)
            {
                var diff = (go.transform.parent.position - transform.position).sqrMagnitude;
                var goRb = go.GetComponent<KeplerOrbitMover>().OrbitData;

                Vector3 goVel = new Vector3((float)goRb.Velocity.x, (float)goRb.Velocity.y, (float)goRb.Velocity.z);
                float angleOfAttack = Vector3.Dot(goVel, go.transform.parent.position - transform.position);


                if (diff < distance /*&& angleOfAttack < 0 && goVel.sqrMagnitude > fastestAsteroidSpeed && diff <= range*/)

                {

                    distance = diff;
                    fastestAsteroidSpeed = goVel.sqrMagnitude;

                    if (distance < range && fastestAsteroidSpeed <= goVel.sqrMagnitude)
                    {
                        planetTargets.Push(go.transform);
                        //target = go.transform;

                    }

                }
            }
            else
            {
                var diff = (go.transform.parent.position - transform.position).sqrMagnitude;
                //var goRb = go.GetComponent<KeplerOrbitMover>().OrbitData;

                //Vector3 goVel = new Vector3((float)goRb.Velocity.x, (float)goRb.Velocity.y, (float)goRb.Velocity.z);
                //float angleOfAttack = Vector3.Dot(goVel, go.transform.parent.position - transform.position);


                //if (diff >= distance && diff <= range)
                //{

                //    //distance = diff;
                //    //fastestAsteroidSpeed = goVel.sqrMagnitude;

                //    //if (distance < range && fastestAsteroidSpeed <= goVel.sqrMagnitude)
                //    //{
                        planetTargets.Push(go.transform);
                        //target = go.transform;

                    //}

                //}
            }
               

        }
            //if(target != null)
            //    planetTargets.Add(target);
        //    yield return new WaitForSecondsRealtime(0.2f);
        ////}
       


    }








}









































    //public static Vector3 CalculateInterceptCourse(Vector3 aTargetPos, Vector3 aTargetSpeed, Vector3 aInterceptorPos, float aInterceptorSpeed, out bool aSuccess)
    //{
    //    aSuccess = true;
    //    Vector3 targetDir = aTargetPos - aInterceptorPos;
    //    float iSpeed2 = aInterceptorSpeed * aInterceptorSpeed;
    //    float tSpeed2 = aTargetSpeed.sqrMagnitude;
    //    float fDot1 = Vector3.Dot(targetDir, aTargetSpeed);
    //    float targetDist2 = targetDir.sqrMagnitude;
    //    float d = (fDot1 * fDot1) - targetDist2 * (tSpeed2 - iSpeed2);
    //    if (d < 0.1f)
    //        aSuccess = false;
    //    float sqrt = Mathf.Sqrt(d);
    //    float S1 = (-fDot1 - sqrt) / targetDist2;
    //    float S2 = (-fDot1 + sqrt) / targetDist2;
    //    if (S1 < 0.0001f)
    //    {
    //        if (S2 < 0.0001f)
    //            return Vector3.zero;
    //        else
    //            return (S2) * targetDir + aTargetSpeed;
    //    }
    //    else if (S2 < 0.0001f)
    //        return (S1) * targetDir + aTargetSpeed;
    //    else if (S1 < S2)
    //        return (S2) * targetDir + aTargetSpeed;
    //    else
    //        return (S1) * targetDir + aTargetSpeed;
    //}

    //void AcquireTargetLock(GameObject target)
    //{
    //    bool acquireTargetLockSuccess;
    //    Rigidbody playerShipScript = target.GetComponent<Rigidbody>();
    //    Vector3 targetVelocity = playerShipScript.velocity;
    //    Vector3 direction = CalculateInterceptCourse(target.transform.position, targetVelocity, gameObject.transform.position, 500, out acquireTargetLockSuccess);
    //    if (acquireTargetLockSuccess)
    //    {
    //        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
    //        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 1f);
    //        if (Mathf.Abs(gameObject.transform.rotation.eulerAngles.y - targetRotation.eulerAngles.y) < 5)
    //            SimplePool.Spawn(GameManager.Instance.missile, gameObject.transform.position, Quaternion.identity);
    //    }
    //}

//}
