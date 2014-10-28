using UnityEngine;
using System.Collections;

public class CheckDest : MonoBehaviour {

	public int insideBoxCount;
	
	void Start () {
		insideBoxCount = 0;
	}

	void OnTriggerEnter(Collider other){
		insideBoxCount++;
	}
}
