using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debree : MonoBehaviour
{


    [SerializeField]
    private bool homing = false;
    [SerializeField]
    private Transform homingTrans;

    [SerializeField]
    private float speed  = 0.05f;

    [SerializeField]
    private int scoreValue;

    private Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

        Vector3 direction = gameObject.transform.position - AsteroidSpawner.Instance.gameObject.transform.position;

        
        rb.velocity = direction * Random.Range(0.01f, speed);

    }




    private void Update()
    {
        if(homing && homingTrans != null)
        {
            transform.position = homingTrans.position;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Missile") && !homing)
        {
            homing = true;
            homingTrans = other.transform;
           
            //SimplePool.Despawn(gameObject);
            //gameObject.transform.SetParent(other.gameObject.transform);

            //gameObject.tag = "DebreeHome";
        }
        if (other.gameObject.CompareTag("Life") && homing)
        {
            //Float text spawn, rotate to camera
            GameObject tmp = SimplePool.Spawn(AsteroidSpawner.Instance.fltText, gameObject.transform.position, Quaternion.identity);
            tmp.transform.LookAt(Camera.main.transform);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format("+{0}", scoreValue.ToString());

            GameManager.Instance.money += scoreValue;
            GameManager.Instance.moneyText.text = GameManager.Instance.money.ToString();


            homing = false;
            homingTrans = null;
            Destroy(gameObject);
        }
    }

}
