using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacer : MonoBehaviour {

    private Camera camera;
   
    // Use this for initialization
    void Start () {
         camera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
}
