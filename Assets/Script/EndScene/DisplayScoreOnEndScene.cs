using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScoreOnEndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Text> ().text =	"Level " + GameStateManager.m_instance.m_level + " Completed\n" +
											"Cow killed : " + GameStateManager.m_instance.m_CowKilled +"\n"+
											"Game duration : " + GameStateManager.m_instance.m_playTime  +" seconds\n"+
											"Alert started : " + GameStateManager.m_instance.m_alerteStarted;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
