using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public GameObject colliders;
	public Material envMatNew;

	void OnCollisionEnter() {
		colliders.SetActive(true);
		gameObject.renderer.material = envMatNew;

	}
}
