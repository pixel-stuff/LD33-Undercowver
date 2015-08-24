using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayButtonNextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameStateManager.m_instance.m_isEndSceneSuccess){
			this.GetComponent<Text> ().text =	"Next Level";
		}else{
			this.GetComponent<Text> ().text =	"Retry";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
