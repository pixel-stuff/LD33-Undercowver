using UnityEngine;
using System.Collections;

public class Beam_borders_collider : MonoBehaviour {

	public bool isBorderTop = false;
	public Beam parent = null;

	public GameObject particuleCaptureFeedback;

	private Cow liftCow;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!particuleCaptureFeedback) {
			particuleCaptureFeedback = GameObject.FindGameObjectWithTag ("ParticuleCaptureFeedback");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Cow cow = other.GetComponent<Cow> ();
		if (isBorderTop) {
			if (cow!=null && !cow.getIsUFOCatched() && parent.isCowInBeam(cow)) {
				liftCow = cow;
				liftCow.setCowState(CowState.Lifted);
				Debug.Log ("Lifted");
				liftCow.hideAndFreeze();
				//particuleCaptureFeedback.SetActive(true);
				Invoke("releaseCow",1); 
			}
		}
	}

	public void releaseCow(){
		//particuleCaptureFeedback.SetActive( false);
		liftCow.setCowAtPosition(this.transform.position);
		liftCow.showAndUnfreeze ();
	}
}
