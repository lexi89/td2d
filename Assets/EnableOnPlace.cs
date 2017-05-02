using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnPlace : MonoBehaviour {

	public void OnPlace(){
		GetComponent <Tower> ().enabled = true;
	}
}
