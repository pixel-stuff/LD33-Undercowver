using UnityEngine;
using System.Collections;

public class Beam_borders_collider : MonoBehaviour {

	public bool isBorderTop = false;
	public Beam parent = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (isBorderTop) {
			if (parent.getCatched()!=null && other.GetComponent<Cow> ()!=null) {
				parent.getCatched().setCowState(CowState.Lifted);
				parent.docked();
			}
		} else {
			if (parent.getCatched()!=null && other.GetComponent<Cow> ()!=null) {
				parent.getCatched().setCowState(CowState.Crashed);
				parent.releaseCatched();
			}
		}
	}
}
