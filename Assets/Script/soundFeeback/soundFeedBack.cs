using UnityEngine;
using System.Collections;

public class soundFeedBack : MonoBehaviour {
	public float alertNoisePercent;
	public float stopalertNoisePercent;


	private bool isOnAlertNoise;
	public void setSoundPercent(float percent){
		if (percent > alertNoisePercent) {
			isOnAlertNoise = true;
			//todo warning machin 
		}
		if (percent < stopalertNoisePercent) {
			//todo if true hide warning
			isOnAlertNoise = false;
		}
		soundFeedbackBar[] scripts = gameObject.GetComponentsInChildren<soundFeedbackBar> ();

		for (int i= 0; i < scripts.Length; i++) {
			scripts [i].setPercent (percent);
		}

	}
}
