using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Tower : MonoBehaviour {

	public static int widthInUnits;
	public static int heightInUnits;

	public Sprite menuImage;
	public Sprite level1;
	public Sprite level2;
	public Sprite level3;

	Color originalColor;
	Material material;

	void Awake(){
		material = GetComponent <MeshRenderer> ().material;
		originalColor = material.color;
	}

	void Start(){
	}

	public void place(){
		GetComponent <NavMeshObstacle>().enabled = true;
	}

	public void cantPlace(){
		material.color = Color.red;
	}

	public void canPlace(){
		material.color = originalColor;
	}
	
}
