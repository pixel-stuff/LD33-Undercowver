using UnityEngine;
using System.Collections;

public class UILevelManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}



	
	// Update is called once per frame
	void Update () {
	
	}


	public void ReturnToSceneMenu(){
		GameStateManager.m_instance.GoToSceneMenu ();
	}

	public void GoToEndSceneWithLoose(){
		GameStateManager.m_instance.GoToEndSceneWithLoose ();
	}

	public void GoToEndSceneWithSuccess(){
		GameStateManager.m_instance.GoToEndSceneWithSuccess ();
	}
}
