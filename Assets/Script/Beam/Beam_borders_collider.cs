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
			if (other.GetComponent<Cow> ()!=null && !other.GetComponent<Cow> ().getIsUFOCatched()) {
				other.GetComponent<Cow>().setCowState(CowState.Lifted);
			}
		} else {
			if (other.GetComponent<Cow> ()!=null) {
				other.GetComponent<Cow>().setCowState(CowState.Crashed);
			}
		}
	}
}
