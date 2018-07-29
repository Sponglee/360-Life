using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {
	public float lifeTime;
	void Start()
	{
        AudioManager.Instance.PlaySound("explosion_asteroid");
		Destroy (gameObject, lifeTime);
	}
}
