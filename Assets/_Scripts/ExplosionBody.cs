using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBody : MonoBehaviour {

    public string soundName;


    private void OnEnable()
    {
        AudioManager.Instance.PlaySound(soundName);
    }
}
