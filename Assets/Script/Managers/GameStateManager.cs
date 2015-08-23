using UnityEngine;
using System.Collections;
using System;

public enum GameState{
	Menu,
	Playing,
	Pause,
	EndScene
}

public class GameStateManager : MonoBehaviour {

	#region Singleton
	public static GameStateManager m_instance;
	void Awake(){
		if(m_instance == null){
			//If I am the first instance, make me the Singleton
			m_gameState = GameState.Menu;
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != m_instance)
				Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	private static GameState m_gameState;
	
	public static Action<GameState> onChangeStateEvent;


	/*
	 * Nbre cow killed
	 * temps de jeu
	 * Nbre d'alerte déclanché
	 * 
	 * 
	 */

	// Use this for initialization
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("GAME STATE : " + m_gameState);
		if (m_asynLoading != null) {
			//Debug.Log ("LOADING : " + m_asynLoading.progress);
			//Debug.Log ("is done : " + m_asynLoading.isDone + "(" + m_asynLoading.progress*100f +"%)" );
		}
		/*if (Time.time - timeStartLoading >= 10f) {
			m_asynLoading.allowSceneActivation = true;
		}*/
	}

	public static GameState getGameState(){
		return m_gameState;
	}

	public static void setGameState(GameState state){
		m_gameState = state;
		if(onChangeStateEvent != null){
			onChangeStateEvent(state);
		}
	}

	public void GoToSceneMenu(){
		this.setGameState (GameState.Menu);
		Application.LoadLevelAsync ("MenuScene");
	}

	AsyncOperation m_asynLoading;
	float timeStartLoading;

	public void GoToEndSceneWithLoose(){
		this.setGameState (GameState.EndScene);
		m_asynLoading =  Application.LoadLevelAsync ("EndScene");

		//m_asynLoading.allowSceneActivation = false;
		timeStartLoading = Time.time;
	}

	public void GoToEndSceneWithSuccess(){
		this.setGameState (GameState.EndScene);
		m_asynLoading =  Application.LoadLevelAsync ("EndScene");
		
		//m_asynLoading.allowSceneActivation = false;
		timeStartLoading = Time.time;
	}

	public void GoToLevelScene (){
		this.setGameState (GameState.EndScene);
		m_asynLoading =  Application.LoadLevelAsync ("LevelScene");
		
		//m_asynLoading.allowSceneActivation = false;
		timeStartLoading = Time.time;
	}
}
