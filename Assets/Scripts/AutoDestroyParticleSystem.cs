﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {

	ParticleSystem ps;

	void Awake(){
		ps = GetComponent <ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(ps){
			if(!ps.IsAlive ()){
				Destroy (transform.parent.gameObject);
			}
		}	
	}
}
