using UnityEngine;
using System.Collections;

public class HouseLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (other.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
