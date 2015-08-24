using UnityEngine;
using System.Collections;

public class Beam_borders_collider : MonoBehaviour {

	public bool isBorderTop = false;
	public Beam parent = null;

	private Cow liftCow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Cow cow = other.GetComponent<Cow> ();
		if (isBorderTop) {
			if (cow!=null && !cow.getIsUFOCatched() && parent.isCowInBeam(cow)) {
				liftCow = cow;
				liftCow.setCowState(CowState.Lifted);
				Debug.Log ("Lifted");

				liftCow.hideAndFreeze();

				Invoke("releaseCow",1); 
			}
		}
	}

	public void releaseCow(){
		liftCow.setCowAtPosition(this.transform.position);
		liftCow.showAndUnfreeze ();
	}
}
