using UnityEngine;
using System.Collections;

public class soundFeedbackBar : MonoBehaviour {

	public float m_initialScale;

	// Use this for initialization
	void Start () {
	}
	
	public void setPercent(float percent) {
		Color actualColor = this.GetComponent<SpriteRenderer> ().color;
		if (percent < 35) {
			this.GetComponent<SpriteRenderer> ().color = new Color(actualColor.r,actualColor.g,actualColor.b,percent /35);
		} else {
			this.GetComponent<SpriteRenderer> ().color = new Color(actualColor.r,actualColor.g,actualColor.b,percent /35);;
		}

		if (percent > 100) {
			percent = 100;
		}
		this.transform.localScale = new Vector3 (1, m_initialScale * (percent / 100), 1);
	}
}
