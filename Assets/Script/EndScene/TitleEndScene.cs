using UnityEngine;
using System.Collections;

public class TitleEndScene : MonoBehaviour {
	
	[SerializeField]
	private GameObject m_gameOver;

	[SerializeField]
	private GameObject m_missionAccomplished;

	// Use this for initialization
	void Start () {
		if (GameStateManager.m_instance.m_isEndSceneSuccess) {
			m_missionAccomplished.SetActive(true);
		} else {
			m_gameOver.SetActive(true);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
