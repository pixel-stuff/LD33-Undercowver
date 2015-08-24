using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class soundFeedBack : MonoBehaviour {
	public float alertNoisePercent;
	public float stopalertNoisePercent;
	public GameObject AlertSection;
	public GameObject textPercent;

	private Text percentText;
	private bool isOnAlertNoise;

	void Start(){
		percentText = textPercent.GetComponent<Text> ();
	}


	public void setSoundPercent(float percent){
		if (percent > alertNoisePercent || isOnAlertNoise) {
			isOnAlertNoise = true;
			percentText.text = Mathf.Round(percent)+"%";
			AlertSection.SetActive(true); 
		}
		if (percent < stopalertNoisePercent) {
			AlertSection.SetActive(false);
			isOnAlertNoise = false;
		}
		soundFeedbackBar[] scripts = gameObject.GetComponentsInChildren<soundFeedbackBar> ();

		for (int i= 0; i < scripts.Length; i++) {
			scripts [i].setPercent (percent);
		}

	}
}
