using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Baddy : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		GetComponent<NavMeshAgent>().destination = GameController.Instance.Castle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
