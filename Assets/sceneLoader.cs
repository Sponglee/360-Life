﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("YAY");
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
