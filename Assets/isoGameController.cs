using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isoGameController : MonoBehaviour {
	public static isoGameController instance;
	public GameObject TilePrefab;
	public Transform Tiles;
	public Transform Castle;
	[SerializeField] int width;
	[SerializeField] int height;

	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
