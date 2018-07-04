using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FltText : MonoBehaviour {
    
    public float timer;

    public void Start()
    {
        
        {
            timer = Random.Range(1f,1.2f);
        }
    }
    // Update is called once per frame
    void Update () {
		if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.localPosition -= new Vector3 (0, -0.005f, 0);
            transform.localScale += new Vector3(0.001f, 0.001f, 0);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
