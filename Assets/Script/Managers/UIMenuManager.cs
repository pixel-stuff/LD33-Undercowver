using UnityEngine;
using System.Collections;

public class UIMenuManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void GoToLevelScene(){
		GameStateManager.m_instance.GoToLevelScene ();
	}
}
