using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetModelRotator : MonoBehaviour {

    public float speed = 0.2f;

    // Update is called once per frame
    void Update () {
        gameObject.transform.Rotate(Vector3.up, speed);
	}
}
