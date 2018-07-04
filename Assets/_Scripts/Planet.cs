using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {


    //[SerializeField]
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

    private Transform planetTarget;
    public Transform PlanetTarget
    {
        get
        {
            return planetTarget;
        }

        set
        {
            planetTarget = value;
        }
    }


    public float pewCoolDown = 2f;

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



    //number of simultaneous missiles per planet
    private int missileCount=0;
    public int MissileCount
    {
        get
        {
            return missileCount;
        }

        set
        {
            missileCount = value;
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        pewCoolDown -= Time.deltaTime;
		if(gameObject.CompareTag("Life") && pewCoolDown<0 && GameManager.Instance.money >= GameManager.Instance.missileCost && MissileCount<=GameManager.Instance.missileLimit)
        {
            GameManager.Instance.money -= GameManager.Instance.missileCost;
            GameManager.Instance.moneyText.GetComponent<Text>().text = GameManager.Instance.money.ToString();
            pewCoolDown = 2f;

            if (PrepeareFire())
            {
                MissileCount++;
                GameObject tmp = SimplePool.Spawn(GameManager.Instance.missile, gameObject.transform.position, Quaternion.identity);
                tmp.transform.SetParent(transform);
            }
           
         
        }
	}



    private bool PrepeareFire()
    {
        //yield return new WaitForSecondsRealtime(fuseDelay);

     


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            var diff = (go.transform.parent.position - transform.position).sqrMagnitude;

           // Debug.Log(diff + " : " + distance);

            if (diff < Range)
            {
                planetTarget = go.transform;
                return true;

            }
        }

        return false;
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
