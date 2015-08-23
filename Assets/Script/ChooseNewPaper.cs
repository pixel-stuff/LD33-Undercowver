using UnityEngine;
using System.Collections;

public class ChooseNewPaper : MonoBehaviour {
	
	[SerializeField]
	private GameObject m_successNewspaper;

	[SerializeField]
	private GameObject m_looseNewspaper;

	// Use this for initialization
	void Start () {
		if(GameStateManager.getGameState() == GameState.EndSceneGameOver){
			m_looseNewspaper.SetActive(true);
		}else{
			m_successNewspaper.SetActive(true);
		}
		this.GetComponent<Animator> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
