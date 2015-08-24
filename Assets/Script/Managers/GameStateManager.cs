using UnityEngine;
using System.Collections;
using System;

public enum GameState{
	Menu,
	Playing,
	Pause,
	EndSceneGameOver,
	EndSceneSuccess
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

	public bool alreadyHaveTuto = false;

	public bool m_isEndSceneSuccess = false;

	public int m_level;
	public int m_CowKilled = 0;
	public float m_playTime = 0;
	public float m_alerteStarted = 0;
	/*
	 * Nbre cow killed
	 * temps de jeu
	 * Nbre d'alerte déclanché
	 * 
	 * 
	 */

	// Use this for initialization
	void Start () {
		m_level = 0;
	}


	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("GAME STATE : " + m_gameState);
		if (m_asynLoading != null) {
			//Debug.Log ("LOADING : " + m_asynLoading.progress);
			//Debug.Log ("is done : " + m_asynLoading.isDone + "(" + m_asynLoading.progress*100f +"%)" );
			if (m_asynLoading.progress >= 0.90f) {
				try{
					m_asynLoading.allowSceneActivation = true;
					m_asynLoading = null;
				}catch(Exception exp){
					Debug.Log ("ERROR : " + exp.ToString());
				}
			}
		}

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
	float m_timeStartLevel;

	public void GoToEndSceneWithLoose(){
		m_isEndSceneSuccess = false;
		m_playTime = Time.time - m_timeStartLevel;
		this.setGameState (GameState.EndSceneGameOver);
		m_asynLoading =  Application.LoadLevelAsync ("EndScene");
		m_level--;
		//m_asynLoading.allowSceneActivation = false;
		//m_timeStartLevel = Time.time;
	}

	public void GoToEndSceneWithSuccess(){
		m_isEndSceneSuccess = true;
		m_playTime = Time.time - m_timeStartLevel;
		this.setGameState (GameState.EndSceneSuccess);
		m_asynLoading =  Application.LoadLevelAsync ("EndScene");
		
		//m_asynLoading.allowSceneActivation = false;
		//m_timeStartLevel = Time.time;
	}

	public void GoToLevelScene (){
		this.setGameState (GameState.Playing);
		m_level++;
		/*m_asynLoading = */ Application.LoadLevelAsync ("LevelScene");
		AudioManager.PlayBacgoundMusic ();
		//m_asynLoading.allowSceneActivation = false;
		m_timeStartLevel = Time.time;
	}

	public int getNumberOfCowToLoad(){
		return 5 * m_level;
	}
}
