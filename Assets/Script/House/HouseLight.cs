using UnityEngine;
using System.Collections;

public class HouseLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "spaceShipSprite") {
			PlayerManager.m_instance.setGameOver();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
