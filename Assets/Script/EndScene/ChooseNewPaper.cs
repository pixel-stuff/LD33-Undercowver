using UnityEngine;
using System.Collections;

public class ChooseNewPaper : MonoBehaviour {
	
	[SerializeField]
	private GameObject m_successNewspaper;

	[SerializeField]
	private GameObject m_looseNewspaper;

	public float m_alphaApplier = 0.0f;

	[SerializeField]
	private GameObject m_UIEndScene;

	[SerializeField]
	private SpriteRenderer m_cache;

	private Color m_colorToAppli;
	
	[SerializeField]
	private GameObject[] m_listeNewsPaperLoose;
	
	[SerializeField]
	private GameObject[] m_listeNewsPaperSuccess;

	// Use this for initialization
	void Start () {
		m_colorToAppli = new Color (0f, 0f, 0f, 0f);
		if(GameStateManager.getGameState() == GameState.EndSceneGameOver){
			m_looseNewspaper.SetActive(true);
		}else{
			m_successNewspaper.SetActive(true);
		}
		this.GetComponent<Animator> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		m_colorToAppli.a = m_alphaApplier;
		m_cache.color = m_colorToAppli;
	}

	private int m_newPaperActivate = 0;
	public void ActivateNewsPaper(){
		if (GameStateManager.getGameState () == GameState.EndSceneGameOver) {
			m_listeNewsPaperLoose[m_newPaperActivate].SetActive(true);
		} else {
			m_listeNewsPaperSuccess[m_newPaperActivate].SetActive(true);
		}
		m_newPaperActivate++;
	}




	public void EndAnimation(){
		m_UIEndScene.SetActive (true);
	}

}
